using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorScheduleDto
    {
        public int DoctorId { get; set; }
        //public Doctor Doctor { get; set; }
        public bool IsWorking { get; set; }
        public DateTime WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TimeInterval { get; set; }
        //public DoctorScheduleDto()
        //{
        //    StartTime = WeekDay.TimeOfDay.ToString();
        //    EndTime = WeekDay.TimeOfDay.ToString();
        //}
    }

    public class DoctorEditScheduleDto : DoctorScheduleDto
    {
        public int Id { get; set; }
    }
}
