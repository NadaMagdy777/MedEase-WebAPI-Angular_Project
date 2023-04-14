using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorAppointmentAndPatternDto
    {
        public int Pattern { get; set; }
        public List<DateTime> ReservedAppointmanet{ get; set; }
        public DateTime WeekDay { get; set; } 
        public string StartTime { get; set; } 
        public string EndTime { get; set; } 
    }
}
