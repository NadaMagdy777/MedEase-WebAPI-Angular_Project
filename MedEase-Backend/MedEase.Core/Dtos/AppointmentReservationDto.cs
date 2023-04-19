using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class AppointmentReservationDto
    {
        public DateTime Date { get; set; }
        public virtual int PatientID { get; set; }
        public virtual int DoctorID { get; set; }
        public bool HasInsurance { get; set; }
        public int InsurancesId { get; set; }
        public bool HasInvestigations { get; set; }
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public string Image { get; set; }       //==> array of bytes
    }
}
