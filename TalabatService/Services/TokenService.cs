using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.Identity;
using TalabatCore.ITokenService;

namespace TalabatService.Services
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> Create(AppUser user, UserManager<AppUser> userManager)
        {
            //private claims
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)

            };
            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole) {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            //key
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:validIssuer"],
                audience: configuration["JWT:validAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims:authClaims,
                signingCredentials:new SigningCredentials(authkey,SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
