using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.DTOs;
using Talabat.Extensions;
using Talabat.Helper;
using TalabatCore.Entities.Identity;
using TalabatCore.ITokenService;

namespace Talabat.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTo>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(new UserDTo()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.Create(user, userManager),
            });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDto registerDto)
        {
            if(CheckEmailExists(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "This Email is already in use" } });
            var user = new AppUser() {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new UserDTo()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.Create(user, userManager),

            });
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTo>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            return Ok(new UserDTo() { DisplayName = user.DisplayName, Email = user.Email, Token = await tokenService.Create(user, userManager) });
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto UpdatedAddress)
        {
            var address = mapper.Map<AddressDto, Address>(UpdatedAddress);
            // inside this function we get user email by claimsType
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            user.Address = address;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400, "An Error"));
            return Ok(mapper.Map<Address, AddressDto>(user.Address));
        }
        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            return Ok(mapper.Map<Address, AddressDto>(user.Address));

        }
        
        [HttpGet("CheckEmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var user=await userManager.FindByEmailAsync(email);
            return user != null;
        }
    }
}
