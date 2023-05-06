using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Answer { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAnswered { get; set; }
        public int SpecialityId { get; set; }
        public int PatientId { get; set; }
        public int? DoctorId { get; set; }
        public string DocName { get; set; }
    }
}
