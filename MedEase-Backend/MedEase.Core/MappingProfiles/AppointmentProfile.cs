using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedEase.Core.Dtos;
using Microsoft.AspNetCore.Http;

namespace MedEase.Core.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, ReserveAppointmentDto>().ReverseMap();
            CreateMap<AppointmentReservationDto, Appointment>().ForMember(dest=>dest.Date,option=>option.MapFrom(src=> DateTime.Parse(src.Date))).ReverseMap();
            CreateMap<Appointment, AppointmentStatusDto>().ReverseMap();
        }
    }
}
