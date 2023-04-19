using MedEase.Core.Consts;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorRegisterDto : UserRegisterDto
    {
        public bool AllowVisa { get; set;}

        [Required, Range(0, 10000)]
        public float Fees { get; set; }

        //[Required]
        //public byte[] LicenseImg { get; set; }

        //[Required]
        //public byte[] ProfilePicture { get; set; }

        [Required, MinLength(2), MaxLength(200)]
        public string Faculty { get; set; }

        [Required]
        public int SpecialityID { get; set; }
        public List<int>? SubSpecialities { get; set; }
        public List<int>? Insurances { get; set; }
    }
}
