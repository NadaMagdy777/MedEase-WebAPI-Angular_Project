using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorScheduleDto
    {
        public int DoctorId { get; set; }
        //public Doctor Doctor { get; set; }
        public bool IsWorking { get; set; } = false;
        public DateTime WeekDay { get; set; } = DateTime.Now;
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int TimeInterval { get; set; } = 10;
        public DoctorScheduleDto()
        {
            StartTime = WeekDay.TimeOfDay.ToString();
            EndTime = WeekDay.TimeOfDay.ToString();
        }
    }
}
