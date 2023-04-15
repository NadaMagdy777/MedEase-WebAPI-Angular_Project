using AutoMapper;
using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces;
using MedEase.Core.Interfaces.Services;
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
        //private readonly SignInManager<AppUser> _signInManager;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly ITokenGenerator _tokenGenerator;
        private readonly IAccountService _accountService;

        public AccountController
            (
        //    SignInManager<AppUser> signInManager,
        //    UserManager<AppUser> userManager, 
        //    ITokenGenerator tokenGenerator,
            IAccountService accountService
            )
        {
            //    this._signInManager = signInManager;
            //    this._tokenGenerator = tokenGenerator;
            this._accountService = accountService;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<ApiResponse>> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _accountService.LoginUser(dto));
        }

        [HttpPost("Doctors/register")]
        public async Task<ActionResult<ApiResponse>> PatientRegister(DoctorRegisterDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _accountService.RegisterDoctor(dto));
        }

    //[HttpPost ("Patient/register")]
    //public async Task<ActionResult<ApiResponse>> DoctorRegister(UserRegisterDto dto)
    //{
    //    if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

    //    AppUser user = new()
    //    {
    //        FirstName = "Abdallah",
    //        LastName = "Assaker",
    //        Email = "Abdallah@gmail.com",
    //        UserName = "Abdallah@gmail.com"
    //    };
    //    var result = await _userManager.CreateAsync(user, "123aaaASD@#%");
    //    if(!result.Succeeded) { return BadRequest(new ApiResponse(400, false, result.Errors)); }

    //    return Ok(new ApiResponse(200, true, new UserDto
    //    {
    //        Name = "Test",
    //        Email = user.Email,
    //        Token = await _tokenGenerator.GenerateToken(user, 3),
    //    }));
    //}

    }   
}
