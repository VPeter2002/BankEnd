// -----------------------------------------------------------------
// A VÉGLEGES, JAVÍTOTT Startup.cs
// -----------------------------------------------------------------

using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using AD41HN_HFT_2022231.Repository.ModelRepositories;
using AD41HN_HFT_2022231.Db.Data;
using AD41HN_HFT_2022231.Endpoint.Services; // <-- EZ KELL
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Logging;
using System.Linq; // FONTOS: Hozzáadva az .Any() metódushoz
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Endpoint
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // =================================================================
        // ConfigureServices (Ez már jó volt, csak kicsit formáztam)
        // =================================================================
        public void ConfigureServices(IServiceCollection services)
        {
            // --- A TE EREDETI LOGIKÁD (Adatbázis, Repók, Logikák) ---
            services.AddDbContext<FWCDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("FWCConnection"));
            });

            services.AddTransient<IRepository<Doctor>, DoctorRepository>();
            services.AddTransient<IRepository<CareSensAirData>, CareSensAirDataRepository>();
            services.AddTransient<IRepository<OhioGlucose>, OhioGlucoseRepository>();
            services.AddTransient<IRepository<OhioMeal>, OhioMealRepository>();
            services.AddTransient<IRepository<OhioInsulin>, OhioInsulinRepository>();
            services.AddTransient<IRepository<OhioExercise>, OhioExerciseRepository>();
            services.AddTransient<IRepository<Patient>, PatientRepository>();

            services.AddTransient<IDoctorLogic, DoctorLogic>();
            services.AddTransient<ICareSensAirDataLogic, CareSensAirDataLogic>();
            services.AddTransient<IOhioGlucoseLogic, OhioGlucoseLogic>();
            services.AddTransient<IOhioMealLogic, OhioMealLogic>();
            services.AddTransient<IOhioInsulinLogic, OhioInsulinLogic>();
            services.AddTransient<IOhioExerciseLogic, OhioExerciseLogic>();
            services.AddTransient<IPatientLogic, PatientLogic>();

            services.AddSignalR();
            services.AddControllers();

            // --- Swagger ---
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AD41HN_HFT_2022231.Endpoint", Version = "v1" });
            });

            // --- CORS ---
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials()); 
            });

            // --- AUTENTIKÁCIÓS RÉSZ ---

            // 1. Identity Adatbázis (SQLite)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // 2. Identity beállítása (Laza jelszóval)
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 3. JWT Hitelesítés
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // Ha a kérés a SignalR Hub-ra megy (/hub), akkor vegyük ki a tokent az URL-ből
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hub")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // =================================================================
        // Configure (JAVÍTOTT SORRENDDEL ÉS ÖSSZEVONT LOGIKÁVAL)
        // =================================================================
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ------------------------------------------------------
            // 1. LÉPÉS: Adatbázisok Létrehozása és Feltöltése (Seeding)
            // ------------------------------------------------------
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Először hozzuk létre az Identity (Felhasználói) adatbázist
                    var identityContext = services.GetRequiredService<ApplicationDbContext>();
                    identityContext.Database.EnsureCreated();

                    // Másodszor hozzuk létre a Diabétesz adatbázist
                    var fwcContext = services.GetRequiredService<FWCDbContext>();
                    fwcContext.Database.EnsureCreated();

                    // HARMADSZOR: Most, hogy az adatbázis létezik, feltölthetjük (ha üres)
                    if (!fwcContext.OhioGlucose.Any()) // Feltételezve, hogy a DbSet neve 'OhioGlucoses'
                    {
                        var logger = services.GetRequiredService<ILogger<Startup>>();
                        logger.LogInformation("Adatbázis üres, Seeding (feltöltés) indul...");

                        DbSeeder.SeedCareSensData(fwcContext);
                        DbSeeder.SeedXmlPatientData(fwcContext, "C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend másolata\\AD41HN_HFT_2022231.Repository\\XML563.xml"); // JAVÍTSD AZ ELÉRÉSI UTAT, HA KELL!
                        DbSeederOhio.ImportJsonToDatabase(fwcContext);

                        logger.LogInformation("Seeding (feltöltés) befejezve.");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "Hiba történt az adatbázisok (EnsureCreated) létrehozása vagy feltöltése (Seeding) közben.");
                }
            }

            // ------------------------------------------------------
            // 2. LÉPÉS: A HTTP Kérések Kezelése (Pipeline)
            // ------------------------------------------------------

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AD41HN_HFT_2022231.Endpoint v1"));
            }

            // Globális Hiba Kezelés
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseHttpsRedirection();

            // FONTOS SORREND:
            app.UseRouting();

            app.UseCors("AllowSpecificOrigin"); // CORS

            app.UseAuthentication(); // Hitelesítés (Ki vagy?)
            app.UseAuthorization();  // Jogosultságkezelés (Mit tehetsz?)

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // API Controller-ek
                endpoints.MapHub<SignalRHub>("/hub"); // A jövőbeli csevegés helye
            });
        }
    }
}