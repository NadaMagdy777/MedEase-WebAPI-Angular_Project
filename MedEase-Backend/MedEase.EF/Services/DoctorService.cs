using MedEase.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEase.Core.Dtos;


namespace MedEase.EF.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public DoctorAppointmentAndPatternDto GetPatternAndAppointment()
        {

            // return _unitOfWork.
        }

        //internal class DoctorService            //شيل القرف ده
        //{
        //    //get ==> pattern -> DoctorSchedule class, post for creation 
        //    //get ==> appointment -> appointment , post for reservation 

        //}
    }
