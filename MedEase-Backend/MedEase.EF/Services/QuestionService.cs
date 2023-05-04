using MedEase.Core.Dtos;
using MedEase.Core.Models;
using MedEase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedEase.Core.Interfaces.Services;

namespace MedEase.EF.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetQuestionsByDoctorSpeciality(int docId)
        {
            int? docSpecId = await _unitOfWork.Doctors.FindDtoAsync(d => d.ID == docId, d => d.SpecialityID);

            if (docSpecId == null) { return new ApiResponse(400, false); }

            IEnumerable<Question> questions = await _unitOfWork.Questions
                .FindAllAsync(q => q.SpecialityId == docSpecId.Value && !q.IsAnswered);

            return new ApiResponse(200, true, _mapper.Map<IEnumerable<QuestionDto>>(questions).ToList());
        }

        public async Task<ApiResponse?> GetDoctorAnsweredQuestions(int docId)
        {
            IEnumerable<Question> questions =
                await _unitOfWork.Questions.FindAllAsync(q => q.DoctorId == docId);

            return new ApiResponse(200, true, _mapper.Map<IEnumerable<QuestionDto>>(questions).ToList());
        }

        public async Task<ApiResponse> DoctorAnswerQuestion(AnswerDto dto)
        {
            Question question = await _unitOfWork.Questions.FindAsync(q => q.Id == dto.Id);

            if (question == null) { return new ApiResponse(400, false, "Question not found"); }

            question.Answer = dto.Answer;
            question.DoctorId = dto.DoctorId;
            question.IsAnswered = true;

            _unitOfWork.Questions.Update(question);
            _unitOfWork.Complete();

            return (new(200, true, _mapper.Map<QuestionDto>(question)));
        }

        public async Task<ApiResponse> PatientAskQuestion(PatientQuestionDto dto)
        {
            Question question = _mapper.Map<Question>(dto);
            question.DateCreated = DateTime.Now;

            await _unitOfWork.Questions.AddAsync(question);

            _unitOfWork.Complete();

            return new ApiResponse(200, true, _mapper.Map<QuestionDto>(question));
        }

        public async Task<ApiResponse> GetPatientQuestions(int patientID, bool isAnswered)
        {
            IEnumerable<QuestionDto> questions =
                await _unitOfWork.Questions.GetDtoAsync(q => q.PatientId == patientID && q.IsAnswered == isAnswered,
                q => _mapper.Map<QuestionDto>(q));

            return new ApiResponse(200, true);
        }
    }
}
