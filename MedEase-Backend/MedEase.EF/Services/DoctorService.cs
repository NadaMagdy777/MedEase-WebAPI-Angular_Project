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
            var doctors = await _unitOfWork.Doctors.FindAllAsync(d=>true, new List<Expression<Func<Doctor, object>>>()
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
                        doctorDTO = _mapper.Map<DoctorInfoGetDto>(doctor);

                        doctorDTO.Faculty = doctor.Faculty;
                        doctorDTO.addressDto = _mapper.Map<AddressDto>(doctor.AppUser.Address) ;
                        doctorDTO.Name = doctor.AppUser.FirstName + " " + doctor.AppUser.LastName;
                        doctorDTO.age = calucaluteAge(doctor.AppUser.BirthDate);

                        foreach (DoctorSubspeciality subspecialities in doctor.SubSpecialities)
                        {
                            var result =_mapper.Map<SubspecialityDto>(subspecialities.SubSpeciality);
                            doctorDTO.subspecialities.Add(result);

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
        public async Task<bool> EditDoctor(DoctorEditDto doctorDto,int id)
        {
            Doctor doctor=_unitOfWork.Doctors.Find(d=>d.ID==id,
                new List<Expression<Func<Doctor, object>>>()
                {
                   d=>d.AppUser,
               
                });
            //doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.AppUser.FirstName = doctorDto.FirstName;
            doctor.AppUser.LastName = doctorDto.LastName;
            doctor.Fees= doctorDto.Fees;
            doctor.AppUser.PhoneNumber= doctorDto.PhoneNumber;
            doctor.AppUser.Building=doctorDto.Building;
            doctor.AppUser.Street= doctorDto.Street;

            _unitOfWork.Doctors.Update(doctor);
            _unitOfWork.Complete();

            return true;
        }
        public async Task<bool> AddDoctorSubspiciality(int DoctorID,SubspecialityDto subspeciality)
        {
            Doctor doctor = _unitOfWork.Doctors.Find(d => d.ID == DoctorID,
              new List<Expression<Func<Doctor, object>>>()
              {
                 d=>d.SubSpecialities

              });
            SubSpeciality Newsubspeciality = new SubSpeciality();
            Newsubspeciality.Name = subspeciality.name;
            Newsubspeciality.SepcID= subspeciality.SpecialityID;

            await _unitOfWork.SubSpeciality.AddAsync(Newsubspeciality);
            _unitOfWork.Complete();

            DoctorSubspeciality doctorSubspeciality=new DoctorSubspeciality();
            doctorSubspeciality.SubSpeciality = Newsubspeciality;
            doctorSubspeciality.doctor =doctor;
            await _unitOfWork.DoctorSubspeciality.AddAsync(doctorSubspeciality);
            _unitOfWork.Complete();

            return true;
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
    }
}
