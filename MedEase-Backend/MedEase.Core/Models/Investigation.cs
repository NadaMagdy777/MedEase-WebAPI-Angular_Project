using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Investigation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }

        [ForeignKey ("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public InvestigationImage? InvestigationImage { get; set; }
    }
}
