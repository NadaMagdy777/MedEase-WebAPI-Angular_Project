using AutoMapper;
using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _unitOfWork.Complete();

            return new(200, true, appointment);
        }
    }
}
