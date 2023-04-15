using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class CertificationDto
    {
        public int ID { get; set; }
        [Required, MinLength(2), MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Issuer { get; set; }
        public DateTime IssueDate { get; set; }

        public int DoctorID { get; set; }
    }
}
