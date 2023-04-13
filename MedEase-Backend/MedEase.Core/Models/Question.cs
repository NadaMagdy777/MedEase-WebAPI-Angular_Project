using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAnswered { get; set; }
        public int SpecialityId { get; set; }
        public int PatientId { get; set; }
        public int? DoctorId { get; set; }
        public Speciality Speciality { get; set; }
        public Patient Patient { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
