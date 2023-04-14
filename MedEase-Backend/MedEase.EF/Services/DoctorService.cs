using MedEase.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEase.Core.Dtos;
using MedEase.Core.Models;
using System.Reflection.Metadata;
using MedEase.Core.Consts;

namespace MedEase.EF.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<List<DoctorAppointmentAndPatternDto>> GetPatternAndAppointmentAsync(int Id)
        {
            List<DoctorAppointmentAndPatternDto> result = new List<DoctorAppointmentAndPatternDto>();
            
            IEnumerable<DoctorSchedule> drs = await _unitOfWork.DoctorSchedule.FindAllAsync(dr => dr.IsWorking == true && dr.DoctorId == Id && dr.WeekDay.Date >= DateTime.Now.Date);

            foreach (var item in drs)
            {
                IEnumerable<DateTime> reservedAppointments = (IEnumerable<DateTime>) await _unitOfWork.Appointments.FindAllWithSelectAsync(app => app.Status != Status.canceled && app.DoctorID == Id && app.Date.Date == item.WeekDay.Date, app => app.Date);
                result.Add(new DoctorAppointmentAndPatternDto() { ReservedAppointmanet = reservedAppointments.ToList(), EndTime = item.EndTime, StartTime = item.StartTime, Pattern = item.TimeInterval, WeekDay = item.WeekDay });
            }
            return result;


            // Appointment ===> Reserved aBlob appointment

            // get ==> pattern -> DoctorSchedule class, post for creation 
            // 
            // return _unitOfWork.
        }

        //internal class DoctorService            //شيل القرف ده
        //{
        //    // get ==> appointment -> appointment , post for reservation 

        //}
    }
}