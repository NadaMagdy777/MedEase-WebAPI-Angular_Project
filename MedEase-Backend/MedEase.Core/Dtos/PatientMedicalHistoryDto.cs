using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class PatientMedicalHistoryDto
    {
        [Required]
        public bool HasAllergies { get; set; }
        [Required]
        public bool HadSurgery { get; set; }
        [Required]
        public bool HasChronicIllnesses { get; set; }
        [Required]
        public bool TakeMedications { get; set; }
        [Required]
        public bool HasHospitalized { get; set; }
        [Required]
        public bool IsSmoking { get; set; }
    }
}
