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
using MedEase.EF.Data;
using System.Drawing.Printing;

namespace MedEase.EF.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _ctx;
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext ctx)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _ctx = ctx;
        }
        public async Task<List<DoctorAppointmentAndPatternDto>> GetPatternAndAppointmentAsync(int Id)
        {

            List<DoctorAppointmentAndPatternDto> result = new List<DoctorAppointmentAndPatternDto>();

            IEnumerable<DoctorSchedule> drs = await _unitOfWork.DoctorSchedule
                .FindAllAsync(dr => dr.IsWorking == true && dr.DoctorId == Id && dr.WeekDay.Date >= DateTime.Now.Date);

            foreach (var item in drs)
            {
                IEnumerable<DateTime> reservedAppointments = await _unitOfWork.Appointments
                    .GetDtoAsync(app => app.Status != Status.canceled && app.DoctorID == Id && app.Date.Date == item.WeekDay.Date,
                    app => app.Date);
                result
                    .Add(new DoctorAppointmentAndPatternDto() {
                        ReservedAppointmanet = reservedAppointments.ToList(), 
                        EndTime = item.EndTime, 
                        StartTime = item.StartTime, 
                        Pattern = item.TimeInterval,
                        WeekDay = item.WeekDay });
            }
            return result;
        }


        public async Task<List<DoctorInfoGetDto>> GetAllDoctors()
        {
            List<DoctorInfoGetDto> doctorsDTOs;

            doctorsDTOs = new List<DoctorInfoGetDto>();

            IEnumerable<Doctor> result = await _unitOfWork.Doctors.FindAllAsync(d => d.IsConfirmed == true);

            foreach (Doctor doctor in result)
            {
                DoctorInfoGetDto doctorDTO = await GetDoctor(doctor.ID);                    //////////          WEIRD       <<<================
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
                doctorDTO.age = CalucaluteAge(doctor.AppUser.BirthDate);
                doctorDTO.DoctorcerInsurance = await GetDoctorInsurranecs(doctor.ID);
                doctorDTO.DoctorSubspiciality = await GetDoctorSubspiciality(doctor.ID);
                doctorDTO.Doctorcertificates = _mapper.Map<List<CertificateDto>>(doctor.Certificates);
                doctorDTO.WaitingTime =await CaluclutDoctorWaitingTime(doctor.ID);
                doctorDTO.Rating = await CaluclutDoctorRating(doctor.ID);
                doctorDTO.visitors = await GetCountOfDoctorPatients(doctor.ID);
                doctorDTO.ClincRating=await CaluclutClincRating(doctor.ID);


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

        public int CalucaluteAge(DateTime birthDate)
        {
            DateTime dataNow = DateTime.Today;

            int age = dataNow.Year - birthDate.Year;

            if (dataNow.Month < birthDate.Month || (dataNow.Month == birthDate.Month && dataNow.Day < birthDate.Day))
                age--;

            return age;
        }

        public async Task<IEnumerable<ReviewDto>> GetDoctorReviews(int Id)
        {
            //IEnumerable<ReviewDto> reviews1 = await _unitOfWork.Reviews
            //    .GetDtoAsync(r => r.Examination.DoctorID == Id, r => _mapper.Map<ReviewDto>(r));
            
              var reviewsList= await _unitOfWork.Reviews.FindAllAsync(R => R.Examination.DoctorID == Id,
               new List<Expression<Func<Review, object>>>()
               {
                   R=>R.Examination.Patient.AppUser


               }); 
             List<ReviewDto> reviews = _mapper.Map<List<ReviewDto>>(reviewsList.ToList());

            return reviews;
        }

        public async Task<ApiResponse> CreateReview(ReviewDto dto)
        {
            int? ExaminationID = await _unitOfWork.Examinations.FindDtoAsync(e => e.AppointmentID == dto.AppointmentID, e => e.ID);
            if (ExaminationID.Value == 0) { return new(404, false); }

            Review review = _mapper.Map<Review>(dto);
            review.ExaminationID = ExaminationID.Value;

            review = await _unitOfWork.Reviews.AddAsync(review);

            _unitOfWork.Complete();

            return new(200, true, _mapper.Map<ReviewDto>(review));
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

        public async Task<ApiResponse> GetSpecialities()
        {
            return new ApiResponse(200, true, _mapper.Map<IEnumerable<SpecialityDto>>(await _unitOfWork.Speciality.GetAllAsync()));
        }


        //public async Task<IEnumerable<AppointmentStatusDto>> GetPendingAppointmentsAsync(int docId)   ///asd/asd/asd/asd/asd/asd/               //    DELETE        <<======
        //{
        //    int sepc = (int)await _unitOfWork.Doctors.FindWithSelectAsync(d => d.ID == docId, d => d.SpecialityID);

        //    //IEnumerable<Appointment> pendingAppoints = await FindDoctorAppointments(docId, a => a.Status == Status.doctorPending);
        //    IEnumerable<Appointment> pendingAppoints = await _unitOfWork.Appointments.FindAllAsync(a => a.DoctorID == docId, new()
        //    {
        //        a => a.Investigation,
        //        a => a.Investigation.InvestigationImage,
        //        a => a.Patient.History,
        //    });
        //    return _mapper.Map<IEnumerable<AppointmentStatusDto>>(pendingAppoints);

        //}

        //public async Task<IEnumerable<AppointmentStatusDto>> GetConfirmedAppointmentsAsync(int docId)   ///asd/asd/asd/asd/asd/asd/               //    DELETE        <<======
        //{

        //    IEnumerable<Appointment> confirmedAppoints = await FindDoctorAppointments(docId, a => a.Status == Status.confirmed && a.DoctorConfirmation == true);
        //    return _mapper.Map<IEnumerable<AppointmentStatusDto>>(confirmedAppoints);

        //}

        //public async Task<IEnumerable<Appointment>> FindDoctorAppointments(int docId, Predicate<Appointment> statusCriteria)   ///asd/asd/asd/asd/asd/asd/               //    DELETE        <<======
        //{
        //    Doctor doctor = await _unitOfWork.Doctors.FindAsync(d => d.ID == docId,
        //    new List<Expression<Func<Doctor, object>>>()
        //    {
        //       d => d.Appointments.Select(a => a.Patient.History)
        //    });

        //    return doctor.Appointments.FindAll(statusCriteria).ToList();
        //}

       /* public async Task<ApiResponse> GetPendingAppointmentsAsync2(int docId)   ///asd/asd/asd/asd/asd/asd/               //    Final        <<======
        {
            IEnumerable<DoctorPendingAppointmentDetailsDto> pendingAppoints = await _unitOfWork.Appointments
                .GetDtoAsync(a => a.DoctorID == docId && a.Status == Status.doctorPending,
                a => new DoctorPendingAppointmentDetailsDto
                {
                    AppointmentID = a.ID,
                    PatientID = a.PatientID,
                    PatientName = $"{a.Patient.AppUser.FirstName} {a.Patient.AppUser.LastName}",
                    PatientBirthDate = a.Patient.AppUser.BirthDate.Date,
                    Date = a.Date,
                    PatientGender = a.Patient.AppUser.Gender,
                    PatientPhone = a.Patient.AppUser.PhoneNumber,
                    Status = a.Status,
                    History = _mapper.Map<PatientMedicalHistoryDto>(a.Patient.History),
                    investigation = _mapper.Map<AppointmentInvestigationDto>(a.Investigation),
                },
                dpap => dpap.Date, OrderBy.Descending);



            int? docSpecId = await _unitOfWork.Doctors.FindDtoAsync<int?>(d => d.ID == docId, d => d.SpecialityID);

            foreach (DoctorPendingAppointmentDetailsDto dto in pendingAppoints)
            {
                if (dto.investigation is not null)
                {
                    dto.investigation.Image =
                        (string)                                                                //==> should be byte[]
                        await _unitOfWork.Investigation
                        .FindWithSelectAsync(i => i.AppointmentId == dto.AppointmentID, i => i.InvestigationImage.Image);
                }

                dto.Diagnoses = await _unitOfWork.Diagnosis
                    .GetDtoAsync(d => d.Examination.PatientID == dto.PatientID && d.Examination.Doctor.SpecialityID == docSpecId,
                    d => new DiagnosisDto
                    {
                        Details = d.Details
                    });
            }

            return new ApiResponse(200, true, pendingAppoints);
        }

        public async Task<ApiResponse> GetConfirmedAppointmentsAsync2(int docId)   ///asd/asd/asd/asd/asd/asd/               //    Final        <<======
        {
            IEnumerable<DoctorConfirmedAppointmentDetailsDto> confirmedAppoints = await _unitOfWork.Appointments
                .GetDtoAsync(a => a.Status == Status.patientPending && a.DoctorConfirmation == true && a.DoctorID == docId, 
                a => new DoctorConfirmedAppointmentDetailsDto
                {
                    AppointmentID = a.ID,
                    PatientID = a.PatientID,
                    PatientName = $"{a.Patient.AppUser.FirstName} {a.Patient.AppUser.LastName}",
                    PatientBirthDate = a.Patient.AppUser.BirthDate.Date,
                    Date = a.Date,
                    PatientGender = a.Patient.AppUser.Gender,
                    PatientPhone = a.Patient.AppUser.PhoneNumber,
                    Status = a.Status,                    
                }, 
                dcap => dcap.Date, OrderBy.Descending);

            foreach (DoctorConfirmedAppointmentDetailsDto dto in confirmedAppoints)
            {
                dto.Diagnosis = await _unitOfWork.Diagnosis
                    .FindDtoAsync(d => d.Examination.AppointmentID == dto.AppointmentID,
                    d => new DiagnosisDto
                    {
                        Details = d.Details
                    });

                dto.Prescription = await _unitOfWork.Prescriptions                          ////==> Should return DrugName
                    .GetDtoAsync(p => p.Examination.AppointmentID == dto.AppointmentID,
                    p => new PrescriptionDrugDto
                    {
                        DrugID = p.DrugID,
                        DrugName = p.Drug.Name,
                        Notes = p.Notes,
                        Quantity = p.Quantity,
                    });
            }

            return new(200, true, confirmedAppoints.Where(a => a.Diagnosis == null || a.Prescription == null));
        }*/

        public async Task<PrescriptionDrug> CreatePrescriptionAsync(PrescriptionDrugDto prescriptionDto)
        {
            PrescriptionDrug prescriptionDrug = new PrescriptionDrug();
            prescriptionDrug = _mapper.Map<PrescriptionDrug>(prescriptionDto);

            await _unitOfWork.Prescriptions.AddAsync(prescriptionDrug);
            _unitOfWork.Complete();

            return prescriptionDrug;
        }
        public async Task<DiagnosisDto> CreateDiagnosisAsync(DiagnosisDto diagnosisDto)
        {
            Diagnosis diagnosis = new Diagnosis();
            diagnosis = _mapper.Map<Diagnosis>(diagnosisDto);

            await _unitOfWork.Diagnosis.AddAsync(diagnosis);
            _unitOfWork.Complete();

            return _mapper.Map<DiagnosisDto>(diagnosis);
        }
        public async Task<Examination> CreateExaminationAsync(ExaminationDto examinationDto)   //to be continues
        {
            Examination examination = new Examination();
            examination = _mapper.Map<Examination>(examinationDto);

            await _unitOfWork.Examinations.AddAsync(examination);
            _unitOfWork.Complete();

            return examination;
        }

     

        public async Task<int> CaluclutDoctorWaitingTime(int DocID)
        {
            IEnumerable<Review> Reviews = (IEnumerable<Review>)await _unitOfWork.Reviews
                 .FindAllAsync(r => r.Examination.DoctorID == DocID, new List<Expression<Func<Review, object>>>()
               {
                 r=>r.Examination

               }) ;
            
            var ReviewsList=Reviews.ToList();
            
            int NumOfReviews = ReviewsList.Count;
            int WaitingTimeSum = 0;
            int WaitingTimeAverage = 0;

            if (NumOfReviews > 0)
            {
                foreach (var item in ReviewsList)
                {
                    WaitingTimeSum += item.WaitingTimeinMins;
                }

                 WaitingTimeAverage = WaitingTimeSum / NumOfReviews;
                return WaitingTimeAverage;


            }



            return WaitingTimeAverage;

        }
        public async Task<int> CaluclutDoctorRating(int DocID)
        {
            IEnumerable<Review> Reviews = (IEnumerable<Review>)await _unitOfWork.Reviews
                 .FindAllAsync(r => r.Examination.DoctorID == DocID, new List<Expression<Func<Review, object>>>()
               {
                 r=>r.Examination

               });
           

            

            var ReviewsList = Reviews.ToList();

            int NumOfReviews = ReviewsList.Count;
            int RatingSum = 0;
            int RatingAverage = 0;

            if (NumOfReviews > 0)
            {
                foreach (var item in ReviewsList)
                {
                    RatingSum += item.ClinicRate;
                }

                RatingAverage = RatingSum / NumOfReviews;
                return RatingAverage;


            }

            return RatingAverage;

        }

        public async Task<int> CaluclutClincRating(int DocID)
        {
            IEnumerable<Review> Reviews = (IEnumerable<Review>)await _unitOfWork.Reviews
                 .FindAllAsync(r => r.Examination.DoctorID == DocID, new List<Expression<Func<Review, object>>>()
               {
                 r=>r.Examination

               });

            var ReviewsList = Reviews.ToList();

            int NumOfReviews = ReviewsList.Count;
            int RatingSum = 0;
            int RatingAverage = 0;

            if (NumOfReviews > 0)
            {
                foreach (var item in ReviewsList)
                {
                    RatingSum += item.DoctorRate;
                }

                RatingAverage = RatingSum / NumOfReviews;
                return RatingAverage;


            }

            return RatingAverage;

        }


        public async Task<int> GetCountOfDoctorPatients(int DocId)
        {
            int patientCount =  _unitOfWork.Reviews.Count(A => A.Examination.DoctorID == DocId);
            return patientCount;
                      
        }
    }
}
    