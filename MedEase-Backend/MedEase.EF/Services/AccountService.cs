﻿using AutoMapper;
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



        private readonly IUnitOfWork _unitOfWork;
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private ITokenGenerator _tokenGenerator;

        public AccountService(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ITokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> LoginUser(UserLoginDto dto)
        {
            AppUser user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) { return new ApiResponse(404, false); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) { return new ApiResponse(401, false); }

            int Id = await GetUserTypeId(user);

            return new ApiResponse(200, true, new UserDto
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Token = await _tokenGenerator.GenerateToken(user, Id),
            });
        }

        private async Task<int> GetUserTypeId(AppUser user)
        {
            int? PtId = (int?)await _unitOfWork.Patients
                .FindWithSelectAsync(pt => pt.AppUserID == user.Id, pt => pt.ID);

            if (PtId != null) { return PtId.Value; }


            int? DocId =    (int?) await _unitOfWork.Doctors
                .FindWithSelectAsync(dr => dr.AppUserID == user.Id, dr => dr.ID);

            return DocId.Value;
        }

        public async Task<ApiResponse> RegisterDoctor(DoctorRegisterDto docDto)
        {
            AppUser user = _mapper.Map<AppUser>(docDto);
            Doctor doctor = _mapper.Map<Doctor>(docDto);
            user.Doctor = doctor;
            doctor.AppUser = user;

            doctor.SubSpecialities = docDto.SubSpecialities
                .Select(sDtoId => new DoctorSubspeciality
                {
                    SubspecID = sDtoId,
                    DocID = doctor.ID,
                }).ToList();
            
            doctor.Insurances = docDto.Insurances
                .Select(iDtoId => new DoctorInsurance
                {
                    InsuranceID = iDtoId,
                    DoctorID = doctor.ID,
                }).ToList();

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