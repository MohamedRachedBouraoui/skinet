using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;

namespace API.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var identityBuilder = services.AddIdentityCore<AppUser>();
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, services);
            identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();

            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            // SignInManager  Needs this Authentication service to be declared here
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtParams(configuration["Token:Key"], configuration["Token:Issuer"]));

            return services;
        }

        private static Action<JwtBearerOptions> JwtParams(string tokenKey, string tokenIssuer)
        {
            return opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = true,
                    ValidIssuer = tokenIssuer,
                    //ValidateAudience === Who the token was issued to ?
                    ValidateAudience = false // MUST INIT THIS VALUE (true / false) otherwise the token validation will fail
                };
            };
        }
    }
}