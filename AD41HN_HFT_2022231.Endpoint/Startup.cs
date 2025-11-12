// -----------------------------------------------------------------
// A Startup.cs FÁJL TELJES, JAVÍTOTT TARTALMA
// -----------------------------------------------------------------

using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using AD41HN_HFT_2022231.Repository.ModelRepositories;
using AD41HN_HFT_2022231.Db.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.Configuration;

// -----------------------------------------------------------------
// JAVÍTOTT ÉS SZÜKSÉGES 'USING' UTASÍTÁSOKq C:\Users\Peti ROG\Desktop\Tanulós\Diabetes Webapplication Backend másolata\AD41HN_HFT_2022231.Db.cs\bin\Debug\net5.0\AD41HN_HFT_2022231.Db.dll
// -----------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Logging;

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
        // ConfigureServices (Tisztítva, duplikációk eltávolítva)
        // =================================================================
        public void ConfigureServices(IServiceCollection services)
        {
            // --- A TE EREDETI LOGIKÁD (Adatbázis, Repók, Logikák) ---
            services.AddTransient<FWCDbContext>();
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

            // --- Swagger (Csak egyszer) ---
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AD41HN_HFT_2022231.Endpoint", Version = "v1" });
            });

            // --- CORS (Csak egyszer, a JS frontend címeddel) ---
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            // ------------------------------------------------------
            // ÚJ AUTENTIKÁCIÓS RÉSZ (A 3. LÉPÉSBŐL)
            // ------------------------------------------------------

            // 1. Identity Adatbázis (SQLite) beállítása
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // 2. Identity beállítása
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 3. JWT Hitelesítés beállítása
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
            });
        }

        // =================================================================
        // Configure (Tisztítva, duplikációk és hibás sorrend javítva)
        // =================================================================
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // --- Adatbázis Seeder (SeedOhio hibát dobhat, ha nincs DbContext) ---
            // using (var scope = app.ApplicationServices.CreateScope())
            // {
            //     var context = scope.ServiceProvider.GetRequiredService<FWCDbContext>();
            //     DbSeeder.SeedCareSensData(context);
            //     DbSeeder.SeedXmlPatientData(context, "C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend másolata\\AD41HN_HFT_2022231.Repository\\XML563.xml");
            //     DbSeederOhio.ImportJsonToDatabase(context); // EZT A METÓDUST IS ÁT KELL ÍRNI, HOGY FWCDbContext-et FOGADJON
            // }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AD41HN_HFT_2022231.Endpoint v1"));
            }

            // --- Globális Hiba Kezelés ---
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseHttpsRedirection();

            // ------------------------------------------------------
            // FONTOS SORREND: Routing -> CORS -> Auth -> Endpoints
            // ------------------------------------------------------

            app.UseRouting();

            // CORS (A megadott policy névvel)
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication(); // Hitelesítés
            app.UseAuthorization();  // Jogosultságkezelés

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // API Controller-ek
                // endpoints.MapHub<SignalRHub>("/hub"); // IDE KELL A TE HUB OSZTÁLYOD NEVE
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Kérjük el az ApplicationDbContext-et
                    var identityContext = services.GetRequiredService<ApplicationDbContext>();
                    identityContext.Database.EnsureCreated(); // <-- Ez a varázslat!

                    // Kérjük el a te FWCDbContext-edet (ha azt is így akarod létrehozni)
                    var fwcContext = services.GetRequiredService<FWCDbContext>();
                    fwcContext.Database.EnsureCreated(); // <-- Ezzel a másik adatbázisod is létrejön
                }
                catch (Exception ex)
                {
                    // Hiba esetén írjuk ki a logba (opcionális, de hasznos)
                    var logger = services.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "Hiba történt az adatbázisok (EnsureCreated) létrehozása közben.");
                }
            }
        }
    }
}