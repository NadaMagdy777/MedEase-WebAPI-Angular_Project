using MedEase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


    internal class DoctorService
    {
        //get ==> pattern -> DoctorSchedule class, post for creation 
        //get ==> appointment -> appointment , post for reservation 

    }
}
