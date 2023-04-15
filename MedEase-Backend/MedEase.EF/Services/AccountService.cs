using AutoMapper;
using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces;
using MedEase.Core.Interfaces.Services;
using MedEase.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;



        //private readonly IUnitOfWork _unitOfWork;
        //private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private ITokenGenerator _tokenGenerator;
        //private IAccountService _accountService;

        public AccountService(
        //    SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ITokenGenerator tokenGenerator,
            IMapper mapper
            )
        {
            //    this._signInManager = signInManager;
            this._userManager = userManager;
            this._tokenGenerator = tokenGenerator;
            _mapper = mapper;

        }

        public async Task<ApiResponse> RegisterDoctor(DoctorRegisterDto docDto)
        {
            AppUser user = new();
            user = _mapper.Map<AppUser>(docDto);
            //user.UserName = new MailAddress(docDto.Email).User;

            Doctor doctor = new();
            doctor = _mapper.Map<Doctor>(docDto);

            user.Doctor = doctor;
            doctor.AppUser = user;

            if (docDto.SubSpecialities != null && docDto.SubSpecialities.Count != 0)
            {
                doctor.SubSpecialities = new();
                foreach (int subSpecId in docDto.SubSpecialities)
                {
                    DoctorSubspeciality docSubSpec = new()
                    {
                        SubspecID = subSpecId,
                        DocID = doctor.ID,
                    };

                    doctor.SubSpecialities.Add(docSubSpec);
                }
            }

            if (docDto.Insurances != null && docDto.Insurances.Count != 0)
            {
                doctor.Insurances = new();
                foreach (int insId in docDto.Insurances)
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
                result = await _userManager.CreateAsync(user, docDto.Password);
            }
            catch (Exception)
            {
                return new ApiResponse(400, false, null, "InValid Inputs");
            }

            if (!result.Succeeded) { return new ApiResponse(400, false, result.Errors); }      //result.Errors

            await _userManager.AddToRoleAsync(user, Roles.Doctor.ToString());

            return new ApiResponse(200, true, new UserDto
            {
                Name = $"{docDto.FirstName} {docDto.LastName}",
                Email = user.Email,
                Token = await _tokenGenerator.GenerateToken(user, doctor.ID),
            });
        }
    }
}
