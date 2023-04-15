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
            CreateMap<Doctor, DoctorInfoGetDto>().ReverseMap();
            CreateMap<AppUser,DoctorInfoGetDto>().ReverseMap();
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<SubSpeciality , SubspecialityDto>().ReverseMap();



        }

    }
}
