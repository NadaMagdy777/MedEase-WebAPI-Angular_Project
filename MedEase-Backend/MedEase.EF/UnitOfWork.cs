using MedEase.Core;
using MedEase.Core.Interfaces;
using MedEase.Core.Interfaces.Repositories;
using MedEase.Core.Models;
using MedEase.EF.Data;
using MedEase.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Appointment> Appointments { get; private set; }
        public IBaseRepository<Doctor> Doctors { get; private set; }
        public IBaseRepository<DoctorSchedule> DoctorSchedules { get; private set; }
        public IBaseRepository<Patient> Patients { get; private set; }

        public IBaseRepository<subSpeciality> subSpeciality { get; private set; }

        public IBaseRepository<Speciality> Speciality { get; private set; }

        public IBaseRepository<Certificates> certificate { get; private set; }

        public IBaseRepository<Insurance> insurance { get; private set; }

        public IBaseRepository<DoctorInsurance> DoctorInsurance { get; private set; }
        public IBaseRepository<PatientInsurance> PatientInsurance { get; private set; }
        public IBaseRepository<DoctorSubspeciality> DoctorSubspeciality { get; private set; }









        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Appointments = new BaseRepository<Appointment>(_context);
            Doctors = new BaseRepository<Doctor>(_context);
            DoctorSchedules = new BaseRepository<DoctorSchedule>(_context);
            Patients = new BaseRepository<Patient>(_context);
            subSpeciality = new BaseRepository<subSpeciality>(_context);
            Speciality = new BaseRepository<Speciality>(_context);
            certificate = new BaseRepository<Certificates>(_context);
            insurance = new BaseRepository<Insurance>(_context);
            DoctorInsurance = new BaseRepository<DoctorInsurance>(_context);
            PatientInsurance = new BaseRepository<PatientInsurance>(_context);
            DoctorSubspeciality = new BaseRepository<DoctorSubspeciality>(_context);






        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}