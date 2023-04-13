using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Review
    {
        public int ID { get; set; }
        private string? Comment { get; set; }
        
        [Required, Range(1,5)]
        public int DoctorRate { get; set; }

        [Required, Range(1, 5)]
        public int ClinicRate { get; set; }

        [Required, Range(1, 300)]
        public int WaitingTimeinMins { get; set; }
    }
}
