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
    public class ExaminationProfile : Profile
    {
        public ExaminationProfile()
        {
            CreateMap<Examination, ExaminationDto>().ReverseMap();
            CreateMap<PrescriptionDrug, PrescriptionDrugDto>().ReverseMap();
            CreateMap<Diagnosis, DiagnosisDto>()
                .ForMember(src => src.ExaminationID, opt => opt.MapFrom(dest => dest.ExaminationID))
                .ReverseMap();
        }
    }
}
