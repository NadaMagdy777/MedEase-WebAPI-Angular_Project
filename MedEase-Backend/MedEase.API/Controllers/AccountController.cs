using MedEase.Core.Dtos;
using MedEase.Core.Interfaces;
using MedEase.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenGenerator tokenGenerator;

        public AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, ITokenGenerator _tokenGenerator)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            tokenGenerator = _tokenGenerator;
        }

        [HttpPost ("login")]
        public async Task<ActionResult<ApiResponse>> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            AppUser user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) { return Unauthorized(new ApiResponse(401)); }

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if(!result.Succeeded) { return Unauthorized(new ApiResponse(401)); }

            return Ok(new UserDto
            {
                Name = "Test",
                Email = user.Email,
                Token = await tokenGenerator.GenerateToken(user),
            });
        }


        [HttpPost ("register")]
        public async Task<ActionResult<ApiResponse>> Register(UserRegisterDto dto)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            AppUser user = new()
            {
                FirstName = "Abdallah",
                LastName = "Assaker",
                Email = "Abdallah@gmail.com",
                UserName = "Abdallah@gmail.com"
            };
            var result = await userManager.CreateAsync(user, "123aaaASD@#%");
            if(!result.Succeeded) { return BadRequest(new ApiResponse(400)); }

            return Ok(new UserDto
            {
                Name = "Test",
                Email = user.Email,
                Token = await tokenGenerator.GenerateToken(user),
            });
        }

        [HttpGet("secret")]
        [Authorize]
        public string getSecret()
        {
            return "secret sting";
        }

    }
}
