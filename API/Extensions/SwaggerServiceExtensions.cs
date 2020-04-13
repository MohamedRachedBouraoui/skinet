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
            services.AddSwaggerGen(s =>
                       {
                           s.SwaggerDoc("v1", new OpenApiInfo { Title = "Skinet-API", Version = "v1" });
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