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
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, AnswerDto>().ReverseMap();
            CreateMap<Question, PatientQuestionDto>().ReverseMap();
        }
    }
}
