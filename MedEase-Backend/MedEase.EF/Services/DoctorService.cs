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
using System.Text.RegularExpressions;
using System.Numerics;

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

            doctorsDTOs = new List<DoctorInfoGetDto>();

            var result = (List<Doctor>)await _unitOfWork.Doctors.FindAllWithSelectAsync(d => d.IsConfirmed == true);

            foreach (Doctor doctor in result)
            {


                DoctorInfoGetDto doctorDTO = await GetDoctor(doctor.ID);
                doctorsDTOs.Add(doctorDTO);
            }





            return doctorsDTOs;
        }

        public async Task<DoctorInfoGetDto> GetDoctor(int ID)
        {
            DoctorInfoGetDto doctorDTO = null;

            Doctor doctor = _unitOfWork.Doctors.Find(d => d.ID == ID,
               new List<Expression<Func<Doctor, object>>>()
               {
                   d=>d.AppUser,
                   d=>d.Insurances,
                   d=>d.Certificates,
                   d=>d.SubSpecialities,
                   d=>d.Speciality,
                   d=>d.AppUser.Address

               });
            if (doctor != null)
            {
                doctorDTO = new DoctorInfoGetDto();
                doctorDTO = _mapper.Map<DoctorInfoGetDto>(doctor);
                doctorDTO.addressDto = _mapper.Map<AddressDto>(doctor.AppUser.Address);
                doctorDTO.age = calucaluteAge(doctor.AppUser.BirthDate);
                doctorDTO.DoctorcerInsurance = await GetDoctorInsurranecs(doctor.ID);
                doctorDTO.DoctorSubspiciality = await GetDoctorSubspiciality(doctor.ID);
                doctorDTO.Doctorcertificates = _mapper.Map<List<CertificateDto>>(doctor.Certificates);

            }
            return doctorDTO;

        }


        public async Task<DoctorSchedule> CreateScheduleAsync(DoctorScheduleDto scheduleDto)
        {
            if (scheduleDto.IsWorking)
            {
                DoctorSchedule schedule = new DoctorSchedule();
                schedule = _mapper.Map<DoctorSchedule>(scheduleDto);

                await _unitOfWork.DoctorSchedules.AddAsync(schedule);
                _unitOfWork.Complete();

                return schedule;

            }

            return null;

        }
        public async Task<int> EditDoctor(DoctorEditDto doctorDto, int id)
        {
            Doctor doctor = _unitOfWork.Doctors.Find(d => d.ID == id && d.IsConfirmed == true,
                new List<Expression<Func<Doctor, object>>>()
                {
                   d=>d.AppUser,

                });

            if (doctor != null)
            {
                //doctor = _mapper.Map<Doctor>(doctorDto);
                doctor.AppUser.FirstName = doctorDto.FirstName;
                doctor.AppUser.LastName = doctorDto.LastName;
                doctor.Fees = doctorDto.Fees;
                doctor.AppUser.PhoneNumber = doctorDto.PhoneNumber;
                doctor.AppUser.Building = doctorDto.Building;
                doctor.AppUser.Street = doctorDto.Street;
                doctor.ProfilePicture = doctorDto.ProfilePicture;
                doctor.AppUser.Address.City = doctorDto.City;
                doctor.AppUser.Address.Region = doctorDto.Region;

                _unitOfWork.Doctors.Update(doctor);
                return _unitOfWork.Complete();



            }
            return 0;

        }
        public async Task<int> AddDoctorSubspiciality(int DoctorID, SubspecialityDto subspeciality)
        {
            Doctor doctor = _unitOfWork.Doctors.Find(d => d.ID == DoctorID,
              new List<Expression<Func<Doctor, object>>>()
              {
                 d=>d.SubSpecialities

              });
            SubSpeciality Newsubspeciality = new SubSpeciality();
            Newsubspeciality = _mapper.Map<SubSpeciality>(subspeciality);

            await _unitOfWork.SubSpeciality.AddAsync(Newsubspeciality);
            _unitOfWork.Complete();

            DoctorSubspeciality doctorSubspeciality = new DoctorSubspeciality();
            doctorSubspeciality.SubSpeciality = Newsubspeciality;
            doctorSubspeciality.doctor = doctor;
            await _unitOfWork.DoctorSubspeciality.AddAsync(doctorSubspeciality);
            return _unitOfWork.Complete();

        }
        public async Task<int> AddDoctorCertificate(int DoctorID, CertificateDto certificate)
        {
            Doctor doctor = _unitOfWork.Doctors.Find(d => d.ID == DoctorID,
              new List<Expression<Func<Doctor, object>>>()
              {
                 d=>d.Certificates

              });
            Certificates Newcertificate = new Certificates();
            Newcertificate = _mapper.Map<Certificates>(certificate);
            Newcertificate.Doctor = doctor;


            await _unitOfWork.Certificate.AddAsync(Newcertificate);
            doctor.Certificates.Add(Newcertificate);
            return _unitOfWork.Complete();



        }
        public async Task<int> AddDoctorInsurance(int DoctorID, int insurnceID)
        {

            DoctorInsurance doctorInsurance = new DoctorInsurance();
            doctorInsurance.DoctorID = DoctorID;
            doctorInsurance.InsuranceID = insurnceID;
            await _unitOfWork.DoctorInsurance.AddAsync(doctorInsurance);
            return _unitOfWork.Complete();

        }
        public async Task<List<InsuranceDto>> GetDoctorInsurranecs(int DocId)
        {
            var insurances = await _unitOfWork.DoctorInsurance.FindAllWithSelectAsync(d => d.DoctorID == DocId, d => d.Insurance,
            new List<Expression<Func<DoctorInsurance, object>>>()
             {
                 d=>d.Insurance
             });

            List<InsuranceDto> doctorInsurance = new List<InsuranceDto>();
            doctorInsurance = _mapper.Map<List<InsuranceDto>>(insurances.ToList());

            return doctorInsurance;

        }
        public async Task<List<SubspecialityDto>> GetDoctorSubspiciality(int DocId)
        {
            var subspeciality = await _unitOfWork.DoctorSubspeciality.FindAllWithSelectAsync(d => d.DocID == DocId, d => d.SubSpeciality,
            new List<Expression<Func<DoctorSubspeciality, object>>>()
             {
                 d=>d.SubSpeciality
             });

            List<SubspecialityDto> doctorSubspeciality = new List<SubspecialityDto>();
            doctorSubspeciality = _mapper.Map<List<SubspecialityDto>>(subspeciality.ToList());

            return doctorSubspeciality;



        }

        public int calucaluteAge(DateTime birthDate)
        {
            DateTime dataNow = DateTime.Today;

            int age = dataNow.Year - birthDate.Year;

            if (dataNow.Month < birthDate.Month || (dataNow.Month == birthDate.Month && dataNow.Day < birthDate.Day))
                age--;

            return age;
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
            int? docSpecId = (int?)await _unitOfWork.Doctors.FindWithSelectAsync(d => d.ID == docId, d => d.SpecialityID);

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
        public async Task<ApiResponse> EditScheduleDoctor(int Id, DoctorEditScheduleDto doctorEditScheduleDto)
        {
            DoctorSchedule orgdoctorschedule = await _unitOfWork.DoctorSchedule.FindAsync(d => d.Id == Id);
            if (orgdoctorschedule != null)
            {
                orgdoctorschedule.Id = doctorEditScheduleDto.Id;
                orgdoctorschedule.DoctorId = doctorEditScheduleDto.DoctorId;
                orgdoctorschedule.WeekDay = doctorEditScheduleDto.WeekDay;
                orgdoctorschedule.StartTime = doctorEditScheduleDto.StartTime;
                orgdoctorschedule.EndTime = doctorEditScheduleDto.EndTime;
                orgdoctorschedule.TimeInterval = doctorEditScheduleDto.TimeInterval;
                _unitOfWork.Complete();

                return new ApiResponse(200, true, orgdoctorschedule, "Updated");

            }
            else
            {
                return new ApiResponse(400, false, null, "Bad Request");
            }
        }


        public async Task<IEnumerable<AppointmentStatusDto>> GetPendingAppointmentsAsync(int docId)
        {

            IEnumerable<Appointment> pendingAppoints = await FindDoctorAppointments(docId, a => a.Status == Status.doctorPending);
            return _mapper.Map<IEnumerable<AppointmentStatusDto>>(pendingAppoints);

        }
        public async Task<IEnumerable<AppointmentStatusDto>> GetConfirmedAppointmentsAsync(int docId)
        {

            IEnumerable<Appointment> confirmedAppoints = await FindDoctorAppointments(docId, a => a.Status == Status.confirmed && a.DoctorConfirmation == true);
            return _mapper.Map<IEnumerable<AppointmentStatusDto>>(confirmedAppoints);

        }
        public async Task<IEnumerable<Appointment>> FindDoctorAppointments(int docId, Predicate<Appointment> statusCriteria)
        {
            Doctor doctor = await _unitOfWork.Doctors.FindAsync(d => d.ID == docId,
            new List<Expression<Func<Doctor, object>>>()
            {
               d => d.Appointments.Select(a => a.Patient.History)
            });

            return doctor.Appointments.FindAll(statusCriteria).ToList();
        }
        public async Task<PrescriptionDrug> CreatePrescriptionAsync(PrescriptionDrugDto prescriptionDto)
        {
            PrescriptionDrug prescriptionDrug = new PrescriptionDrug();
            prescriptionDrug = _mapper.Map<PrescriptionDrug>(prescriptionDto);

            await _unitOfWork.Prescriptions.AddAsync(prescriptionDrug);
            _unitOfWork.Complete();

            return prescriptionDrug;
        }
        public async Task<Diagnosis> CreateDiagnosisAsync(DiagnosisDto diagnosisDto)
        {
            Diagnosis diagnosis = new Diagnosis();
            diagnosis = _mapper.Map<Diagnosis>(diagnosisDto);

            await _unitOfWork.Diagnosis.AddAsync(diagnosis);
            _unitOfWork.Complete();

            return diagnosis;
        }
        public async Task<Examination> CreateExaminationAsync(ExaminationDto examinationDto)
        {
            Examination examination = new Examination();
            examination = _mapper.Map<Examination>(examinationDto);

            await _unitOfWork.Examinations.AddAsync(examination);
            _unitOfWork.Complete();

            return examination;
        }

        public async Task<ApiResponse> GetSpecialities()
        {
            return new ApiResponse(200, true, _mapper.Map<IEnumerable<SpecialityDto>>(await _unitOfWork.Speciality.GetAllAsync()));
        }
    }
}