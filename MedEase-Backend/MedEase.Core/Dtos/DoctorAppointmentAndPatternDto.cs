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
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 
    }
}
