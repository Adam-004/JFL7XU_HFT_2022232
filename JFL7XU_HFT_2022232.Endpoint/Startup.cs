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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace JFL7XU_HFT_2022232.Endpoint
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<SpacecraftOwnershipDBContext>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SpacecraftOwnershipDBContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;

                //options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.WithOrigins("http://localhost:47580")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials());
            });

            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

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
                
                s.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        []
                    }
                });
            });

            services.AddEndpointsApiExplorer();
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

            //app.UseCors(x => x
            //                  .AllowCredentials()
            //                  .AllowAnyMethod()
            //                  .AllowAnyHeader()
            //                  .WithOrigins("http://localhost:47580"));

            app.UseCors();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
