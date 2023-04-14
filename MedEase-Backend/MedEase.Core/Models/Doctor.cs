using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public float Fees { get; set; }
        //public byte[] LicenseImg { get; set; }      //=> ignore
        public string Faculty { get; set; }          //==>get
        public bool IsConfirmed { get; set;}        //==>ignore
        //public byte[] ProfilePicture { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual List<Certificates>? Certificates { get; set; }       //==>get       ==>single in post
        public virtual List<Question> AnsweredQuestions { get; set; }
        public virtual List<DoctorSchedule> Schedule { get; set; }

        [ForeignKey("Speciality")]
        public int SpecialityID { get; set; }
        public virtual Speciality Speciality { get; set; }              //==>get      //=NoPost
        public virtual List<DoctorSubspeciality> SubSpecialities { get; set; }          //==>get    ==>single in post
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<DoctorInsurance> Insurances { get; set; }       //==>get      //=single in post
        public virtual List<Examination> Examinations { get; set; }

    }
}
