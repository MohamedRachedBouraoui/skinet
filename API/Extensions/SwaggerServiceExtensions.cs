using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// API Documentation
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                       {
                           c.SwaggerDoc("v1", new OpenApiInfo { Title = "Skinet-API", Version = "v1" });

                           var securitySchema = new OpenApiSecurityScheme
                           {
                               Description = "JWT Auth Bearer Scheme",
                               Name = "Authorization",
                               In = ParameterLocation.Header,
                               Type = SecuritySchemeType.Http,
                               Scheme = "bearer",
                               Reference = new OpenApiReference
                               {
                                   Type = ReferenceType.SecurityScheme,
                                   Id = "Bearer"
                               }
                           };
                           c.AddSecurityDefinition("Bearer", securitySchema);

                           var securityRequirement = new OpenApiSecurityRequirement
                           {
                                {securitySchema,new[] {"Bearer"}}
                           };

                           c.AddSecurityRequirement(securityRequirement);
                       });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => //to browse to webpage with our api doc (https://localhost:5001/swagger)
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Skinet-API v1");
            });

            return app;
        }
    }
}