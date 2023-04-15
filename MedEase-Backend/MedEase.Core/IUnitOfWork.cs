using MedEase.Core.Interfaces;
using MedEase.Core.Interfaces.Repositories;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Appointment> Appointments { get; }
        public IBaseRepository<Doctor> Doctors { get;  }
        public IBaseRepository<DoctorSchedule> DoctorSchedules { get; }
        public IBaseRepository<Patient> Patients { get; }

        public IBaseRepository<SubSpeciality> SubSpeciality { get; }

        public IBaseRepository<Speciality> Speciality { get; }

        public IBaseRepository<Certificates> Certificate { get; }
        public IBaseRepository<Review> Reviews { get; }

        public IBaseRepository<Insurance> Insurance { get; }

        public IBaseRepository<DoctorInsurance> DoctorInsurance { get; }
        public IBaseRepository<PatientInsurance> PatientInsurance { get; }
        public IBaseRepository<DoctorSubspeciality> DoctorSubspeciality { get; }
        public IBaseRepository<DoctorSchedule> DoctorSchedule { get; }
        public IBaseRepository<Examination> Examinations { get; }

        int Complete();
    }
}