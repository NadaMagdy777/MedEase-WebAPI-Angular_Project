using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class subSpeciality
    {
        public int id { get; set; }
        public string name { get; set; }

        [ForeignKey("speciality")]
        public int SepcID { get; set; }
        public virtual Speciality speciality { get; set; }
        public virtual List<DoctorSubspeciality> doctors { get; set; }

    }
}
