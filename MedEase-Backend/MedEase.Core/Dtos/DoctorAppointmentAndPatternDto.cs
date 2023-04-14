using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorAppointmentAndPatternDto
    {
        public bool IsWorking { get; set; } = false;
        public DateTime WeekDay { get; set; } = DateTime.Now;
        public string StartTime { get; set; } = "";
        public string EndTime { get; set; } = "";
        public int Pattern { get; set; } = 10; //  =====> TimeInterval
    }
}
