using AutoMapper;
using MedEase.Core;
using Microsoft.EntityFrameworkCore;
using MedEase.Core.Dtos;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MedEase.Core.Dtos;
using MedEase.Core.Models;
using System.Reflection.Metadata;
using MedEase.Core.Consts;
using System.Net;

namespace MedEase.EF.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<Appointment> ReserveAppointmentAsync(ReserveAppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment();
            appointment = _mapper.Map<Appointment>(appointmentDto);

            await _unitOfWork.Appointments.AddAsync(appointment);
            _unitOfWork.Complete();

            return appointment;
        }
        public async Task<List<DoctorAppointmentAndPatternDto>> GetPatternAndAppointmentAsync(int Id)
        {

            List<DoctorAppointmentAndPatternDto> result = new List<DoctorAppointmentAndPatternDto>();

            IEnumerable<DoctorSchedule> drs = await _unitOfWork.DoctorSchedule.FindAllAsync(dr => dr.IsWorking == true && dr.DoctorId == Id && dr.WeekDay.Date >= DateTime.Now.Date);


            foreach (var item in drs)
            {
                IEnumerable<DateTime> reservedAppointments = (IEnumerable<DateTime>)await _unitOfWork.Appointments.FindAllWithSelectAsync(app => app.Status != Status.canceled && app.DoctorID == Id && app.Date.Date == item.WeekDay.Date, app => app.Date);
                result.Add(new DoctorAppointmentAndPatternDto() { ReservedAppointmanet = reservedAppointments.ToList(), EndTime = item.EndTime, StartTime = item.StartTime, Pattern = item.TimeInterval, WeekDay = item.WeekDay });
            }
            return result;

        }


        public async Task<List<DoctorInfoGetDto>> GetAll()
        {
            List<DoctorInfoGetDto> doctorsDTOs;
            var doctors = await _unitOfWork.Doctors.FindAllAsync(d => d.ID > 0, new List<Expression<Func<Doctor, object>>>()
            {
               d=>d.AppUser,
               d=>d.Insurances,
               d=>d.Certificates,
               d=>d.SubSpecialities,
               d=>d.Speciality,
               d=>d.AppUser.Address
            });
            doctorsDTOs = new List<DoctorInfoGetDto>();
            if (doctors != null)
            {
                if (doctors.Count() > 0)
                {

                    foreach (Doctor doctor in doctors.ToList())
                    {
                        DoctorInfoGetDto doctorDTO = new DoctorInfoGetDto();
                        doctorDTO.Faculty = doctor.Faculty;
                        doctorDTO.addressDto = doctor.AppUser.Address;
                        doctorDTO.Name = doctor.AppUser.FirstName + " " + doctor.AppUser.LastName;
                        doctorDTO.age = calucaluteAge(doctor.AppUser.BirthDate);
                        doctorDTO.ID = doctor.ID;
                        doctorDTO.Fees = doctor.Fees;
                        doctorDTO.Gender = doctor.AppUser.Gender;
                        doctorDTO.Phone = doctor.AppUser.PhoneNumber;

                        foreach (DoctorSubspeciality subspecialities in doctor.SubSpecialities)
                        {
                            SubspecialityDto subspeciality = new SubspecialityDto();
                            subspeciality.id = subspecialities.SubSpeciality.ID;
                            subspeciality.name = subspecialities.SubSpeciality.Name;

                            doctorDTO.subspecialities.Add(subspeciality);



                        }
                        foreach (Certificates Certificate in doctor.Certificates)
                        {
                            CertificateDto certificate = new CertificateDto();
                            certificate.Title = Certificate.Title;
                            certificate.Description = Certificate.Description;
                            certificate.IssueDate = Certificate.IssueDate;
                            certificate.Issuer = Certificate.Issuer;

                            doctorDTO.certificates.Add(certificate);

                        }

                        foreach (DoctorInsurance innsurances in doctor.Insurances)
                        {
                            doctorDTO.insurance.Add(innsurances.Insurance);


                        }

                        doctorDTO.SpecialityName = doctor.Speciality.Name;




                        doctorsDTOs.Add(doctorDTO);
                    }

                }

            }


            return doctorsDTOs;
        }
       

        public async Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto scheduleDto)
        {
            if (!scheduleDto.IsWorking)
            {
                DoctorSchedule schedule = new DoctorSchedule();
                schedule = _mapper.Map<DoctorSchedule>(scheduleDto);

                await _unitOfWork.DoctorSchedules.AddAsync(schedule);
                _unitOfWork.Complete();

                return schedule;

            }

            return null; 




        }
        public async Task<List<DoctorInsurance>> GetDoctorInsurranecs(int id)
        {
            throw new NotImplementedException();
        }

        public int calucaluteAge(DateTime birtdate)
        {
            DateTime dataNow = DateTime.Today;

            return dataNow.Year - birtdate.Year;
        }

        public async Task<IEnumerable<ReviewDto>> GetDoctorReviews(int Id)
        {
            IEnumerable<ReviewDto> reviews = (IEnumerable<ReviewDto>)await _unitOfWork.Reviews
                .FindAllWithSelectAsync(r => r.Examination.DoctorID == Id, r => _mapper.Map<ReviewDto>(r));

            return reviews;
        }

        public async Task<ReviewDto> CreateReview(ReviewDto dto)
        {
            /////////Check Review ID
            Review review = _mapper.Map<Review>(dto);
            review = await _unitOfWork.Reviews.AddAsync(review);
            _unitOfWork.Complete();
            ReviewDto reviewDto = _mapper.Map<ReviewDto>(review);
            return reviewDto;
        }

        public async Task<ApiResponse> GetQuestionsByDoctorSpeciality(int docId)
        {
            int? docSpecId = (int?) await _unitOfWork.Doctors.FindWithSelectAsync(d => d.ID == docId, d => d.SpecialityID);

            if (docSpecId == null) { return new ApiResponse(404, false, "User Not Found"); }

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

        public async Task<ApiResponse> DoctorAnswerQuestions(AnswerDto dto)
        {
            Question question = await _unitOfWork.Questions.FindAsync(q => q.Id == dto.Id);

            if (question == null) { return new ApiResponse(400, false); }

            question.Answer = dto.Answer;
            question.DoctorId = dto.DoctorId;

            _unitOfWork.Complete();

            return (new(200, true, _mapper.Map<QuestionDto>(question)));
        }
    }
}