using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Patient
    {
        public int ID { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual List<Question> AskedQuestions { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual  PatientInsurance Insurance { get; set; }
        public virtual PatientMedicalHistory History { get; set; }
        public virtual List<Examination> Examinations { get; set; }
    }
}
