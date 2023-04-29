using MedEase.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<ApiResponse> GetQuestionsByDoctorSpeciality(int docId);
        Task<ApiResponse?> GetDoctorAnsweredQuestions(int docId);
        Task<ApiResponse> DoctorAnswerQuestion(AnswerDto dto);
        Task<ApiResponse> PatientAskQuestion(PatientQuestionDto dto);
        Task<ApiResponse> GetPatientQuestions(int patientID, bool isAnswered);
    }
}
