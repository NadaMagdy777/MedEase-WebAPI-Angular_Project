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
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _accountService.LoginUser(dto));
        }

        [HttpPost("Doctors/register")]
        public async Task<ActionResult<ApiResponse>> DoctorRegister(DoctorRegisterDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _accountService.RegisterDoctor(dto));
        }

        [HttpPost("Patient/register")]
        public async Task<ActionResult<ApiResponse>> PatientRegister(UserRegisterDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _accountService.RegisterPatient(dto));
        }

        [HttpGet ("Addresses")]
        public async Task<ActionResult<ApiResponse>> GetAddresses()
        {
            return Ok(await _accountService.GetAddresses());
        }   
    }   
}
