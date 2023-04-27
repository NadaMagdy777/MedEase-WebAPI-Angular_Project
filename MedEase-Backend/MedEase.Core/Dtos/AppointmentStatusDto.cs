using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class AppointmentStatusDto
    {
        public DateTime Date { get; set; }
        public PatientInfoGetDto? Patient { get; set; }
        public AppointmentInvestigationDto? Investigation { get; set; }

    }
}
