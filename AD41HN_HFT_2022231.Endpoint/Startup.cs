using AD41HN_HFT_2022231.Db;
using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Logic.Interfaces;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using AD41HN_HFT_2022231.Repository.ModelRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FWCDbContext>();

            services.AddTransient<IRepository<Player>, PlayerRepository>();
            services.AddTransient<IRepository<Team>, TeamRepository>();
            services.AddTransient<IRepository<Trainer>, TrainerRepository>();
            

            services.AddTransient<IPlayerLogic, PlayerLogic>();
            services.AddTransient<ITeamLogic, TeamLogic>();
            services.AddTransient<ITrainerLogic, TrainerLogic>();

            services.AddControllers();
            object value = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AD41HN_HFT_2022231.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AD41HN_HFT_2022231.Endpoint v1"));

            }
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
