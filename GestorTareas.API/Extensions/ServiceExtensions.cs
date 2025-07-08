using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace GestorTareas.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var key = Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JwtSettings:Emisor"],
                        ValidAudience = config["JwtSettings:Audiencia"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }

        public static void AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce el token como: Bearer {tu_token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });
        }
    }
}
