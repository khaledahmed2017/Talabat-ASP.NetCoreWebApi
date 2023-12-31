using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TalabatCore.Entities.Identity;
using TalabatRepository.Identity;

namespace Talabat.Extensions
{
    public static class ServiceExtension
    {
        
        public static IServiceCollection AddIdentity(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(Options =>
            {
               
            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            //check each user who is signning if he is the owner of this token
            services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/Options => {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer
                (
                Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:validIssuer"],
                        ValidateAudience=true,
                        ValidAudience = configuration["JWT:validAudience"],
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]))
                    };
                }
                );
            return services;
        }
    }
}
