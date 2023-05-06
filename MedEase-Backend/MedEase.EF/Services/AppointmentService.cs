using AutoMapper;
using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<ApiResponse> GetPatientPendingAppointmentsAsync(int patientID)
        {
            IEnumerable<PatientAppointmentDetailsDto> appointments =
               await GetPatientAppointmentsAsync(a => (a.Status == Status.doctorPending || a.Status == Status.patientPending) 
               && a.PatientID == patientID);

            return new(200, true, appointments);
        }
        
        public async Task<ApiResponse> GetPatientConfirmedAppointmentsAsync(int patientID)
        {
            IEnumerable<PatientAppointmentDetailsDto> appointments =
               await GetPatientAppointmentsAsync(a => (a.Status == Status.confirmed || a.Status == Status.DoctorCanceled)
               && a.PatientID == patientID);

            return new(200, true, appointments);
        }

        private async Task<IEnumerable<PatientAppointmentDetailsDto>> GetPatientAppointmentsAsync(Expression<Func<Appointment, bool>> predicate)
        {
            IEnumerable<PatientAppointmentDetailsDto> appointments =
                await _unitOfWork.Appointments.GetDtoAsync(predicate,
                a => new PatientAppointmentDetailsDto
                {
                    AppointmentID = a.ID,
                    Date = a.Date,
                    Status = a.Status,
                    DoctorName = $"{a.Doctor.AppUser.FirstName} {a.Doctor.AppUser.LastName}",
                    DoctorSpeciality = a.Doctor.Speciality.Name,
                },
                ppap => ppap.Date, OrderBy.Descending);

            foreach (PatientAppointmentDetailsDto dto in appointments)
            {
                dto.DiagnosisDetails = await _unitOfWork.Diagnosis.FindDtoAsync(d => d.Examination.AppointmentID == dto.AppointmentID,
                     d => d.Details);

                dto.Prescription = await _unitOfWork.Prescriptions.GetDtoAsync(p => p.Examination.AppointmentID == dto.AppointmentID,
                    p => new PrescriptionDrugDto
                    {
                        DrugID = p.DrugID,
                        DrugName = p.Drug.Name,
                        Notes = p.Notes,
                        Quantity = p.Quantity
                    });

                dto.Reviewd =
                    (await _unitOfWork.Reviews.FindAsync(r => r.Examination.AppointmentID == dto.AppointmentID)) is not null;
            }

            return appointments;
        }

        public async Task<ApiResponse> GetDoctorPendingAppointmentsAsync(int docId)   ///asd/asd/asd/asd/asd/asd/       //    Final        <<======
        {
            IEnumerable<DoctorPendingAppointmentDetailsDto> pendingAppoints = await _unitOfWork.Appointments
                .GetDtoAsync(a => a.DoctorID == docId && a.Status == Status.doctorPending,
                a => new DoctorPendingAppointmentDetailsDto
                {
                    AppointmentID = a.ID,
                    PatientID = a.PatientID,
                    PatientName = $"{a.Patient.AppUser.FirstName} {a.Patient.AppUser.LastName}",
                    PatientBirthDate = a.Patient.AppUser.BirthDate.Date,
                    Date =a.Date.ToString(),
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
                    dto.investigation.Image =                            //==> should be byte[]
                        await _unitOfWork.Investigation
                        .FindDtoAsync(i => i.AppointmentId == dto.AppointmentID, i => i.InvestigationImage.Image);
                }

                dto.PreviousDiagnoses = await _unitOfWork.Diagnosis
                    .GetDtoAsync(d => d.Examination.PatientID == dto.PatientID && d.Examination.Doctor.SpecialityID == docSpecId,
                    d => new DiagnosisDto
                    {
                        Details = d.Details
                    });
            }

            return new ApiResponse(200, true, pendingAppoints);
        }

        public async Task<ApiResponse> GetDoctorConfirmedAppointmentsAsync(int docId)   ///asd/asd/asd/asd/asd/asd/    //    Final        <<======
        {
            IEnumerable<DoctorConfirmedAppointmentDetailsDto> confirmedAppoints = await _unitOfWork.Appointments
                .GetDtoAsync(a => a.Status != Status.DoctorCanceled && a.Status != Status.canceled &&
                    a.Status != Status.doctorPending && a.DoctorConfirmation == true && a.DoctorID == docId,
                a => new DoctorConfirmedAppointmentDetailsDto
                {
                    AppointmentID = a.ID,
                    ExaminationID = a.Examination.ID,
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
                        Details = d.Details,
                        ExaminationID = d.ExaminationID,
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

            return new(200, true, confirmedAppoints);//.Where(a => a.Diagnosis == null || a.Prescription == null));
        }

        public async Task<ApiResponse> ReserveAppointment(AppointmentReservationDto dto)
        {
            Appointment appointment = _mapper.Map<Appointment>(dto);
            appointment.Status = Status.doctorPending;
            appointment.Insurance =
                dto.HasInsurance ? new()
                {
                    InsuranceID = dto.InsurancesId,
                    Appointment = appointment
                } : null;

            appointment.Investigation =
                dto.HasInvestigations ? new()
                {
                    Appointment = appointment,
                    Description = dto.Description,
                    HasImage = dto.HasImage,
                    InvestigationImage =
                        dto.HasImage ? new()
                        {
                            Image = dto.Image,
                        } : null,
                } : null;

            await _unitOfWork.Appointments.AddAsync(appointment);

            try
            {
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return new(500, false, ex.Message);
            }

            return new(201, true, "Appointment Reserved");// appointment);//_mapper.Map<PatientAppointmentDetailsDto>(appointment));    //==>Short Appointment Dto
        }

        public async Task<ApiResponse> DoctorAppointmentAction(AppointmentActionDto dto)
        {
            Appointment appointment = await _unitOfWork.Appointments.FindAsync(a => a.ID == dto.AppointmentID);

            if (appointment == null) { return new(404, false, "Appointment not found"); }

            appointment.Status = Status.patientPending;
            appointment.DoctorConfirmation = dto.Action;

            if (appointment.DoctorConfirmation)
            {
                await CreateExaminationAsync(appointment);
            }

            _unitOfWork.Complete();

            return new(201, true, "Action Set Successfully");
        }

        public async Task<ApiResponse> PatientAppointmentAction(AppointmentActionDto dto)
        {
            Appointment appointment = await _unitOfWork.Appointments.FindAsync(a => a.ID == dto.AppointmentID);

            if (appointment == null) { return new(404, false, "Appointment not found"); }

            switch (dto.Action)
            {
                case true:
                    appointment.Status = appointment.DoctorConfirmation ? Status.confirmed : Status.DoctorCanceled;

                    if (!appointment.DoctorConfirmation) 
                        {await CreateExaminationAsync(appointment);}

                    break;
                case false:

                    if (appointment.Date > DateTime.Now)
                    {
                        appointment.Status = Status.canceled;
                    }
                    else
                    {
                        appointment.Status = appointment.DoctorConfirmation ? Status.PatientCanceled : Status.canceled;
                    }
                    break;
            }

            appointment.PatientConfirmation = dto.Action;

            _unitOfWork.Complete();

            return new(201, true, "Action Set Successfully");
        }

        private async Task<Examination> CreateExaminationAsync(Appointment appointment)
        {
            Examination examination = new()
            {
                DoctorID = appointment.DoctorID,
                PatientID = appointment.PatientID,
                AppointmentID = appointment.ID,
            };
            
            await _unitOfWork.Examinations.AddAsync(examination);

            return examination;
        }
    }
}
