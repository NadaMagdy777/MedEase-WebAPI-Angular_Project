using MedEase.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.EF.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AppointmentInsurance> AppointmentInsurance { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<DoctorInsurance> DoctorInsurance { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedule { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Examination> Examination { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<Investigation> Investigation { get; set; }
        public DbSet<InvestigationImage> InvestigationImage { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PatientInsurance> PatientInsurance { get; set; }
        public DbSet<PatientMedicalHistory> PatientMedicalHistory { get; set; }
        public DbSet<PrescriptionDrug> PrescriptionDrug { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Speciality> Speciality { get; set; }
        public DbSet<SubSpeciality> SubSpeciality { get; set; }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Identity Tables Configurations
            builder.Entity<AppUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            //AppUser Configurations
            builder.Entity<AppUser>().Property<bool>("IsDeleted").HasDefaultValue(false);
            builder.Entity<AppUser>().Property<DateTime>("JoinDate").HasDefaultValueSql("getdate()");
            builder.Entity<AppUser>().HasQueryFilter(a => !Microsoft.EntityFrameworkCore.EF.Property<bool>(a ,"IsDeleted"));


            builder.Entity<DoctorInsurance>()
                  .HasKey(d => new { d.InsuranceID, d.DoctorID });
            
            builder.Entity<DoctorSubspeciality>()
                  .HasKey(d => new { d.SubspecID, d.DocID });
            
            builder.Entity<PrescriptionDrug>()
                  .HasKey(d => new { d.DrugID, d.ExaminationID });
        }
    }
}