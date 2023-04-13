using MedEase.Core.Dtos;
using MedEase.Core.Interfaces;
using MedEase.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

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

        [HttpPost ("/login")]
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


        [HttpPost ("Patient/register")]
        public async Task<ActionResult<ApiResponse>> PatientRegister(DoctorRegisterDto dto)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            AppUser user = new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = new MailAddress(dto.Email).User,
                SSN = dto.SSN,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                PhoneNumber= dto.Phone,
                Address = new()
                {
                    Building = dto.Building,
                    Street = dto.Street,
                    Region = dto.Region,
                    City = dto.City,
                },
                Doctor = new(),
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if(!result.Succeeded) { return BadRequest(new ApiResponse(400)); }      //result.Errors

            return Ok(new UserDto
            {
                Name = $"{dto.FirstName} {dto.LastName}",
                Email = user.Email,
                Token = await tokenGenerator.GenerateToken(user),
            });
        }
        
        [HttpPost ("Patient/register")]
        public async Task<ActionResult<ApiResponse>> DoctorRegister(UserRegisterDto dto)
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

        //DoctorRegister

        [HttpGet("secret")]
        [Authorize]
        public string getSecret()
        {
            return "secret sting";
        }

    }
}
