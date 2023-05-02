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
    public class DoctorInformationProfile:Profile
    {
       public DoctorInformationProfile()
        {
            CreateMap<Doctor,DoctorInfoGetDto>()
                .ForMember(d => d.Building, a => a.MapFrom(s => s.AppUser.Building))
                .ForMember(d => d.Street, a => a.MapFrom(s => s.AppUser.Street))
                .ForMember(d => d.Gender, a => a.MapFrom(s => s.AppUser.Gender))
                .ForMember(d => d.Email, a => a.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.Name, a => a.MapFrom(s => s.AppUser.FirstName+" "+s.AppUser.LastName))
                .ForMember(d => d.PhoneNumber, a => a.MapFrom(s => s.AppUser.PhoneNumber))
                 .ForMember(d => d.SpecialityName, a => a.MapFrom(s => s.Speciality.Name))
                .ReverseMap(); ;
            CreateMap<AppUser, DoctorInfoGetDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<SubspecialityDto, SubSpeciality>()
                .ForMember(dest => dest.specialityID , opt => opt.MapFrom(src => src.SepcID))
                .ReverseMap();
            CreateMap<CertificateDto, Certificates>().ReverseMap();




        }

    }
}
