using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Speciality
    {
        public int id { get; set; }
        public string name { get; set; }

        public virtual List<subSpeciality> subSpecialities { get; set; }
        public virtual List<Doctor> Doctors { get; set; }

    }
}
