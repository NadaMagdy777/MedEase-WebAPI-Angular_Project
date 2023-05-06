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
        public bool IsWorking { get; set; }
        public string WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int TimeInterval { get; set; }
     
    }

    public class DoctorEditScheduleDto : DoctorScheduleDto
    {
        public int Id { get; set; }
    }
}
