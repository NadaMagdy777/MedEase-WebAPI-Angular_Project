using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Investigation
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public Appointment Appointment { get; set; }
    }
}
