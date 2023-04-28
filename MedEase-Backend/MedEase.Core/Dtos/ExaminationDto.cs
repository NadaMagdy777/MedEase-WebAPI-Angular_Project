using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class ExaminationDto
    {
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentID { get; set; }
    }
}
