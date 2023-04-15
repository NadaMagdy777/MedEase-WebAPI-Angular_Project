using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class CertificateDto
    {
        [Required, MinLength(4), MaxLength(10)]
        public string Title { get; set; }

        [Required, MinLength(4), MaxLength(150)]

        public string Description { get; set; }
        
        [Required, MinLength(4), MaxLength(150)]
        public string Issuer { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }
    }
}
