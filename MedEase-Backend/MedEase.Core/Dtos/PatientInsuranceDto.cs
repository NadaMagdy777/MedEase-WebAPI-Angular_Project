using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class PatientInsuranceDto
    {
        [Required]
        public int InsuranceNumber { get; set; }
        public int InsuranceID { get; set; }

    }
}
