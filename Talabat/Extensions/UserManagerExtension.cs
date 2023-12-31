using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TalabatCore.Entities.Identity;

namespace Talabat.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var email =user.FindFirstValue(ClaimTypes.Email);
            var User = await userManager.Users.Include(u=>u.Address).SingleOrDefaultAsync(u=>u.Email==email);
            return User;

        }
    }
}
