using Castle.Core.Configuration;
using JFL7XU_HFT_2022232.Endpoint.Services;
using JFL7XU_HFT_2022232.Logic.Interfaces;
using JFL7XU_HFT_2022232.Logic.Logics;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Repository.Database;
using JFL7XU_HFT_2022232.Repository.Interfaces;
using JFL7XU_HFT_2022232.Repository.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<SpacecraftOwnershipDBContext>();

            services.AddTransient<IRepository<Owner>, OwnerRepository>();
            services.AddTransient<IRepository<Starship>, StarshipRepository>();
            services.AddTransient<IRepository<Hangar>, HangarRepository>();

            services.AddTransient<IOwnerLogic, OwnerLogic>();
            services.AddTransient<IStarshipLogic, StarshipLogic>();
            services.AddTransient<IHangarLogic, HangarLogic>();
            services.AddTransient<INonCrudLogic, NonCrudLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "JFL7XU_HFT_2022232.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JFL7XU_HFT_2022232.Endpoint v1"));
            }

            //For Exceptions to send in Json
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                .Get<IExceptionHandlerPathFeature>()
                .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));
            app.UseCors(x => x
                              .AllowCredentials()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .WithOrigins("http://localhost:22127"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
