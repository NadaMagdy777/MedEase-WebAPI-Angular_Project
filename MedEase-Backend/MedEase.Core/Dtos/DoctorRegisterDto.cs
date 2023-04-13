using MedEase.Core.Consts;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorRegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string SSN { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public float Fees { get; set; }
        public byte[] LicenseImg { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Faculty { get; set; }
        public int Building { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int SpecialityId { get; set; }
        public List<int>? SubSpecialities { get; set; }
        public List<int>? Insurances { get; set; }
    }
}
