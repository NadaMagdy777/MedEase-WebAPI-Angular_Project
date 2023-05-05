using AutoMapper;
using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.Core.Models;
using MedEase.Core.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF.Services
{
    public class PatientService:IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

/*        public async Task<ApiResponse> ReserveAppointment(AppointmentReservationDto dto)
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
            _unitOfWork.Complete();

            return new(200, true, appointment);         //==>Appointment Dto
        }*/

        public async Task<ApiResponse> AddAppointmentInestigation(AppointmentInvestigationDto dto)
        {
            Investigation investigation = new()
            {
                AppointmentId = dto.AppointmentId,
                Description = dto.Description,
                HasImage = dto.HasImage,
                InvestigationImage = dto.HasImage ? new() { Image = dto.Image } : null,
            };

            await _unitOfWork.Investigation.AddAsync(investigation);
            _unitOfWork.Complete();

            return new(200, true, investigation);           //Dto ?? 
        }

        public async Task<bool> EditPatient(PatientEditDto patientDto, int id)
        {
            Patient patient =await _unitOfWork.Patients.FindAsync(d => d.ID == id ,
               new List<Expression<Func<Patient, object>>>()
               {
                   d=>d.AppUser.Address,

               });

            if (patient != null)
            {
                //patient = _mapper.Map<patient>(patientDto);
                patient.AppUser.FirstName = patientDto.FirstName;
                patient.AppUser.LastName = patientDto.LastName;
                patient.AppUser.PhoneNumber = patientDto.PhoneNumber;
                patient.AppUser.Building = patientDto.Building;
                patient.AppUser.Street = patientDto.Street;
                patient.AppUser.AddressID = await GetAddressId(patientDto.City, patientDto.Region);
                patient.AppUser.BirthDate = patientDto.BirthDate;
                patient.AppUser.Email= patientDto.Email;
                

                _unitOfWork.Patients.Update(patient);
                _unitOfWork.Complete();

                return true;
            }
            return false;

        }
        private async Task<int> GetAddressId(string city, string region)
        {
            Address address = await _unitOfWork.Addresses
                .FindAsync(a => a.City == city && a.Region == region);

            return address.ID;
        }
        public async Task<PatientInfoGetDto> GetPatient(int ID)
        {
            PatientInfoGetDto patientDTO = null;

            Patient patient = await _unitOfWork.Patients.FindAsync(d => d.ID == ID,
               new List<Expression<Func<Patient, object>>>()
               {
                   d=>d.AppUser,
                   d=>d.AppUser.Address,
                   d=>d.History,
                   d=>d.Insurance

               });
                if (patient != null)
                {
                    patientDTO = new PatientInfoGetDto();
                    patientDTO = _mapper.Map<PatientInfoGetDto>(patient);
                    patientDTO.History = _mapper.Map<PatientMedicalHistoryDto>(patient.History);



            }
            return patientDTO;

        }

        public async Task<bool> AddPatientInsurance(int PatientID, int InsuranceID)
        {
            Patient patient = await _unitOfWork.Patients.FindAsync(d => d.ID == PatientID,
               new List<Expression<Func<Patient, object>>>()
               {
                   d=>d.AppUser,
                   d=>d.Insurance

               });

                if(patient.Insurance is null)
                {
              

                    PatientInsurance patientInsurance = new PatientInsurance();
                    patientInsurance.InsuranceID=InsuranceID;
                    patientInsurance.PatientID = PatientID;
                    await _unitOfWork.PatientInsurance.AddAsync(patientInsurance);
                    _unitOfWork.Complete();

                    return true;
                }
            return false;
        }

        public async Task<bool> AddMedicalHistory(PatientMedicalHistoryDto medicalHistoryDto,int PatientID)
        {
             Patient patient =await _unitOfWork.Patients.FindAsync(P => P.ID == PatientID,
             new List<Expression<Func<Patient, object>>>()
             {
                   d=>d.AppUser,
                   d=>d.History

             });

            if(patient.History is null)
            {
               PatientMedicalHistory patientMedical=new PatientMedicalHistory();
               patientMedical= _mapper.Map<PatientMedicalHistory>(medicalHistoryDto);
               patientMedical.Patient = patient;
               await _unitOfWork.PatientMedicalHistory.AddAsync(patientMedical);
               patient.History = patientMedical;
               _unitOfWork.Complete();

                return true;
            }

            return false;

        }

        public async Task<bool> EditMedicalHistory(PatientMedicalHistoryDto medicalHistoryDto, int PatientID)
        {
            Patient patient = await _unitOfWork.Patients.FindAsync(P => P.ID == PatientID,
             new List<Expression<Func<Patient, object>>>()
             {
                   d=>d.History
             });

            if(patient.History is not null)
            {
                patient.History.IsSmoking = medicalHistoryDto.IsSmoking;
                patient.History.HadSurgery = medicalHistoryDto.HadSurgery;
                patient.History.HasAllergies = medicalHistoryDto.HasAllergies;
                patient.History.TakeMedications = medicalHistoryDto.TakeMedications;
                patient.History.HasHospitalized = medicalHistoryDto.HasHospitalized;
                patient.History.HasChronicIllnesses = medicalHistoryDto.HasChronicIllnesses;

                _unitOfWork.PatientMedicalHistory.Update(patient.History);
                _unitOfWork.Complete();

                return true;
            }
            return false;
        }


    }
}
