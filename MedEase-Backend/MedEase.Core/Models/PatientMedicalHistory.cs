using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedEase.Core.Models
{
    public class PatientMedicalHistory
    {
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }
        public bool HasAllergies { get; set; }
        public bool HadSurgery { get; set; }
        public bool HasChronicIllnesses { get; set; }
        public bool takeMedications { get; set; }
        public bool HasHospitalized { get; set; }
        public bool IsSmoking { get; set; }
    }
}
