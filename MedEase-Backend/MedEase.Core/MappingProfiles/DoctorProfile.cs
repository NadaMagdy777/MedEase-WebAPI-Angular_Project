using MedEase.Core.Models;
using AutoMapper;
using MedEase.Core.Dtos;

namespace MedEase.Core.MappingProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorRegisterDto, Doctor>()
                .ForMember(dest => dest.LicenseImg, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.SubSpecialities, opt => opt.Ignore())
                .ForMember(dest => dest.SpecialityID, opt => opt.Ignore())
                .ForMember(dest => dest.Insurances, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Doctor, DoctorEditDto>().ReverseMap();
        }
    }
}
