using MedEase.Core.Models;
using AutoMapper;
using MedEase.Core.Dtos;

namespace MedEase.Core.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorRegisterDto>().ReverseMap();
            CreateMap<Doctor, DoctorEditDto>().ReverseMap();
        }
    }
}
