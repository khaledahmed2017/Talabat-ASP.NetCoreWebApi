using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.Identity;

namespace TalabatRepository.Identity
{
    public static class SeedUserAsync
    {
        public static async Task Seeduser(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName="Khaled Ahmed",
                    Email="khaledahemede@gmail.com",
                    UserName="Ahmed",
                    PhoneNumber="0120303030"
                };
                await userManager.CreateAsync(user,"Pa$$word123");
            }
        }
    }
}
