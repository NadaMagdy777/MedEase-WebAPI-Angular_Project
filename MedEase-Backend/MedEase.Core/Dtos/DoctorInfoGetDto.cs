using MedEase.Core.Consts;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
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

        public string Phone { get; set; }

        public Gender Gender { get; set; }
        public int age { get; set; }



        public Address addressDto { get; set; }

        public List<SubspecialityDto> subspecialities { get; set; }

        public string SpecialityName { get; set; }

        public List<CertificateDto> certificates { get; set; }

        public List<Insurance> insurance { get; set; }


    }
}
