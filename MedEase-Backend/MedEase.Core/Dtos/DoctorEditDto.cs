using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorEditDto
    {
        public float Fees { get; set; }
        public string FirstName { get; set; }     
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Building { get; set; }
        public string Street { get; set; }

    }
}
