using AutoMapper;
using MedEase.Core.Dtos;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.MappingProfiles
{
    public class PatientInformationProfile:Profile
    {
        public PatientInformationProfile()
        {
            CreateMap<Patient, PatientInfoGetDto>()
                .ForMember(d => d.Building, a => a.MapFrom(s => s.AppUser.Building))
                .ForMember(d => d.Street, a => a.MapFrom(s => s.AppUser.Street))
                .ForMember(d => d.FirstName, a => a.MapFrom(s => s.AppUser.FirstName))
                .ForMember(d => d.LastName, a => a.MapFrom(s => s.AppUser.LastName))
                .ForMember(d => d.PhoneNumber, a => a.MapFrom(s => s.AppUser.PhoneNumber))
                .ForMember(d => d.Email, a => a.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.BirthDate, a => a.MapFrom(s => s.AppUser.BirthDate))
                .ForMember(d => d.City, a => a.MapFrom(s => s.AppUser.Address.City))
                .ForMember(d => d.Region, a => a.MapFrom(s => s.AppUser.Address.Region))
                .ReverseMap();

            CreateMap<PatientMedicalHistoryDto, PatientMedicalHistory>().ReverseMap();


        }


    }
}
