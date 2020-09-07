using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex.Api.ConfigLan;
using Pokedex.Repository;
using Pokedex.Repository.Persistence;
using Pokedex.Service;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Pokedex.Api.Security;
using System;

namespace Pokedex.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var defaultLanguge = Configuration["DefaultLanguage"];
            var language = Language.GetLanguage(defaultLanguge);

            services.AddSingleton(language);

            var settings = GetJwtSettings();

            services.AddSingleton<JwtSettings>(settings);

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }
                ).AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = settings.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(settings.MinutesToExpiration)

                    };
                });

            services.AddAuthorization(cf =>
                cf.AddPolicy("defaultpolicy", b =>
                {
                    b.RequireAuthenticatedUser();
                })
             );

            var client = Configuration["StaticCliente"];

            services.AddMvcCore();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(client).AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("PokedexApiv1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "PokedexApi",
                    Version = "1"
                });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<PokedexDatabaseCtx>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("cnx_string"));
            });

            services.AddScoped<IDataContextAsync, PokedexDatabaseCtx>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPokedexBaseService, PokedexBaseService>();
            services.AddScoped<IPokedexService, PokedexService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var client = Configuration["StaticCliente"];

            app.UseCors(builder => builder.WithOrigins(client).AllowAnyMethod().AllowAnyHeader());
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/PokedexApiv1/swagger.json", "PokedexApi");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public JwtSettings GetJwtSettings()
        {
            var settings = new JwtSettings
            {
                Key = Configuration["JwtSettings:key"],
                Audience = Configuration["JwtSettings:audience"],
                Issuer = Configuration["JwtSettings:issuer"],
                MinutesToExpiration = int.Parse(Configuration["JwtSettings:minutesToExpiration"])
            };

            return settings;
        }
    }
}
