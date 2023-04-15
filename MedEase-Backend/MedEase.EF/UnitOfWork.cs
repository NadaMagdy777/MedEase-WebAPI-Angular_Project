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
        public IBaseRepository<SubSpeciality> SubSpeciality { get; private set; }
        public IBaseRepository<Speciality> Speciality { get; private set; }
        public IBaseRepository<Certificates> Certificate { get; private set; }
        public IBaseRepository<Insurance> Insurance { get; private set; }
        public IBaseRepository<DoctorInsurance> DoctorInsurance { get; private set; }
        public IBaseRepository<PatientInsurance> PatientInsurance { get; private set; }
        public IBaseRepository<DoctorSubspeciality> DoctorSubspeciality { get; private set; }
        public IBaseRepository<DoctorSchedule> DoctorSchedule { get; private set; }
        public IBaseRepository<Review> Reviews { get; private set; }
        public IBaseRepository<Examination> Examinations { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Appointments = new BaseRepository<Appointment>(_context);
            Doctors = new BaseRepository<Doctor>(_context);
            DoctorSchedules = new BaseRepository<DoctorSchedule>(_context);
            Patients = new BaseRepository<Patient>(_context);
            SubSpeciality = new BaseRepository<SubSpeciality>(_context);
            Speciality = new BaseRepository<Speciality>(_context);
            Certificate = new BaseRepository<Certificates>(_context);
            Insurance = new BaseRepository<Insurance>(_context);
            DoctorInsurance = new BaseRepository<DoctorInsurance>(_context);
            PatientInsurance = new BaseRepository<PatientInsurance>(_context);
            DoctorSubspeciality = new BaseRepository<DoctorSubspeciality>(_context);
            DoctorSchedule = new BaseRepository<DoctorSchedule>(_context);
            Reviews = new BaseRepository<Review>(_context);
            Examinations = new BaseRepository<Examination>(_context);

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