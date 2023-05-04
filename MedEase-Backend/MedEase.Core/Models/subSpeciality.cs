using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class SubSpeciality
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("speciality")]
        public int specialityID { get; set; }
        public virtual Speciality Speciality { get; set; }
        public virtual List<DoctorSubspeciality> Doctors { get; set; }

    }
}
