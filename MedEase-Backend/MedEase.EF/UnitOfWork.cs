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


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
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