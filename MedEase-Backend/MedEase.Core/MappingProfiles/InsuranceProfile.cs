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
    public class InsuranceProfile:Profile
    {
        public InsuranceProfile() {
            CreateMap<InsuranceDto, Insurance>().ReverseMap();
            CreateMap<PatientInsuranceDto, PatientInsurance>().ReverseMap();

        }
       

    }
}
