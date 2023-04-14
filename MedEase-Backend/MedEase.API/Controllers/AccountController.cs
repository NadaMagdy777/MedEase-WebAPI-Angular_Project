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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IAccountService _accountService;

        public AccountController
            (SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, 
            ITokenGenerator tokenGenerator,
            IAccountService accountService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._tokenGenerator = tokenGenerator;
            this._accountService = accountService;
        }

        [HttpPost ("/login")]
        public async Task<ActionResult<ApiResponse>> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid) { return ValidationProblem(ModelState); }

            AppUser user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) { return Unauthorized(new ApiResponse(401, false)); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if(!result.Succeeded) { return Unauthorized(new ApiResponse(401, false)); }

            return Ok(new UserDto
            {
                Name = "Test",
                Email = user.Email,
                Token = await _tokenGenerator.GenerateToken(user),
            });
        }


        [HttpPost ("Doctors/register")]
        public async Task<ActionResult<ApiResponse>> PatientRegister(DoctorRegisterDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            _accountService.RegisterDoctor(dto);

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
                Building = dto.Building,
                Street = dto.Street,
                AddressID = dto.AddressID
            };

            Doctor doctor = new()
            {
                Faculty = dto.Faculty,
                Fees = dto.Fees,
                SpecialityID = dto.SpecialityId
                //LicenseImg = dto.LicenseImg,
                //ProfilePicture = dto.ProfilePicture,
            };

            user.Doctor = doctor;
            doctor.AppUser = user;

            if (dto.SubSpecialities != null && dto.SubSpecialities.Count != 0)
            {
                doctor.SubSpecialities = new();
                foreach (int subSpecId in dto.SubSpecialities)
                {
                    DoctorSubspeciality docSubSpec = new()
                    {
                        SubspecID = subSpecId,
                        DocID = doctor.ID,
                    };

                    doctor.SubSpecialities.Add(docSubSpec);
                }
            }

            if (dto.Insurances != null && dto.Insurances.Count != 0)
            {
                doctor.Insurances = new();
                foreach (int insId in dto.Insurances)
                {
                    DoctorInsurance docins = new()
                    {
                        InsuranceID = insId,
                        DoctorID = doctor.ID,
                    };

                    doctor.Insurances.Add(docins);
                }
            }

            IdentityResult result;
            try
            {
                result = await _userManager.CreateAsync(user, dto.Password);
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse(400, false, null, "InValid Inputs"));
            }

            if(!result.Succeeded) { return BadRequest(new ApiResponse(400, false, result.Errors)); }      //result.Errors

            return Ok(new ApiResponse(200, true, new UserDto
            {
                Name = $"{dto.FirstName} {dto.LastName}",
                Email = user.Email,
                Token = await _tokenGenerator.GenerateToken(user),
            }));
        }
        
        [HttpPost ("Patient/register")]
        public async Task<ActionResult<ApiResponse>> DoctorRegister(UserRegisterDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            AppUser user = new()
            {
                FirstName = "Abdallah",
                LastName = "Assaker",
                Email = "Abdallah@gmail.com",
                UserName = "Abdallah@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "123aaaASD@#%");
            if(!result.Succeeded) { return BadRequest(new ApiResponse(400, false, result.Errors)); }

            return Ok(new ApiResponse(200, true, new UserDto
            {
                Name = "Test",
                Email = user.Email,
                Token = await _tokenGenerator.GenerateToken(user),
            }));
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
