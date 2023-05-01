using MedEase.Core.Consts;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorInfoGetDto
    {
        public int ID { get; set; }


        public float Fees { get; set; }

        public string Faculty { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
        public int age { get; set; }
        public int Building { get; set; }
        public string Street { get; set; }

        public AddressDto addressDto { get; set; }

        public List<SubspecialityDto> DoctorSubspiciality { get; set; }

        public int WaitingTime { get; set; }

        public int Rating { get; set; }

        public int visitors { get; set; }
        public string SpecialityName { get; set; }
        public int ClincRating { get; set; }
        public List<CertificateDto> Doctorcertificates { get; set; }

        public List<InsuranceDto> DoctorcerInsurance { get; set; }


    }
}
