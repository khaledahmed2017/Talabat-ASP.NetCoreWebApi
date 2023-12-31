using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.Identity;

namespace TalabatCore.ITokenService
{
    public interface ITokenService
    {
        Task<string> Create(AppUser user, UserManager<AppUser> signInManager);
    }
}
