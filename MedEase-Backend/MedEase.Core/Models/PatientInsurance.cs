using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class PatientInsurance
    {
        public int ID { get; set; }
        public int InsuranceNumber { get; set; }

        [ForeignKey ("Patient")]
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        
        [ForeignKey ("Insurance")]
        public int InsuranceID { get; set; }
        //public Insurance Insurance { get; set; }
    }
}
