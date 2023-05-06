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
    public class DoctorScheduleProfile : Profile
    {
        public DoctorScheduleProfile()
        {
            CreateMap<DoctorScheduleDto, DoctorSchedule>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndTime)))
                .ForMember(dest => dest.WeekDay, opt => opt.MapFrom(src => DateTime.Parse(src.WeekDay)))
                .ReverseMap();

            CreateMap<DoctorEditScheduleDto, DoctorSchedule>().ReverseMap();
        }
    }
}
