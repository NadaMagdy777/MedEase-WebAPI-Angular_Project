using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorEditDto
    {
        [Required, Range(10, 10000)]
        public float Fees { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(3), MaxLength(30)]

        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(30)]

        public string LastName { get; set; }

        [Required]
        [RegularExpression("^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Please enter valid phone number")]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Building { get; set; }
        
        [Required, MinLength(2), MaxLength(50)]
        public string Street { get; set; }

    }
}
