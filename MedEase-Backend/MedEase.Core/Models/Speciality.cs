using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Speciality
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<subSpeciality> SubSpecialities { get; set; }
        public virtual List<Doctor> Doctors { get; set; }

    }
}
