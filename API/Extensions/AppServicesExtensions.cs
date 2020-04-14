using System;
using System.Collections.Generic;
using System.Linq;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        private static IConfiguration _config;
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            _config = config;
            services.AddControllers();
            ConfigureServicesForDbContext(services);
            ConfigureServicesForRepositories(services);
            ConfigureServicesForAutomapper(services);
            ConfigureServicesForApiBehaviorOptions(services); // MUST BE AFTER  services.AddControllers();

            services.AddSwaggerDoc();
            ConfigureServicesForCORS(services);

            return services;
        }

        private static void ConfigureServicesForCORS(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        /// <summary>
        /// Used to override the APIController deault Behavior with model state Errors
        /// so we can Create our own Invalid ModelState Error Response
        /// </summary>
        /// <param name="services"></param>

        private static void ConfigureServicesForApiBehaviorOptions(IServiceCollection services)
        {
            services.Configure((Action<ApiBehaviorOptions>)(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var modelStateErrors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0);

                    var errors = new List<string>();

                    modelStateErrors.ToList().ForEach(er =>
                    {
                        var values = er.Value.Errors.Select(e => e.ErrorMessage);
                        object ersArray = string.Join('\n', values);

                        errors.Add($"{er.Key}: {ersArray}");
                    });

                    var errorResponse = new ApiValidationErrorResponse { Errors = errors };
                    return new BadRequestObjectResult(errorResponse);
                };
            }));
        }

        private static void ConfigureServicesForAutomapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
        }

        private static void ConfigureServicesForRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static void ConfigureServicesForDbContext(IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
        }

    }
}