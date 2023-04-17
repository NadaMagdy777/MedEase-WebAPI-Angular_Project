using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public bool IsWorking { get; set; } = false;
        public DateTime WeekDay { get; set; } = DateTime.Now;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TimeInterval { get; set; } = 10;
        //public DoctorSchedule()
        //{
        //    StartTime = WeekDay.TimeOfDay.ToString();
        //    EndTime = WeekDay.TimeOfDay.ToString();
        //}

    }
}


