using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int DoctorId { get; set; }
    }
}
