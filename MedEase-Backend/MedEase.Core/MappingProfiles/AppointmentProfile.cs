using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedEase.Core.Dtos;

namespace MedEase.Core.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, ReserveAppointmentDto>().ReverseMap();
            
        }
    }
}
