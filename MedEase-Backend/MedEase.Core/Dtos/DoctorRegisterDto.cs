using MedEase.Core.Consts;
using MedEase.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DoctorRegisterDto : UserRegisterDto
    {
        public bool AllowVisa { get; set;}

        [Required, Range(0, 10000)]
        public float Fees { get; set; }


        //[Required]
        public IFormFile? LicenseImgForm { get; set; }

        //[Required]
        public IFormFile? ProfilePictureForm { get; set; }

        //[ValidateNever, JsonIgnore]
        //public byte[]? LicenseImg { get; set; }

        //[ValidateNever, JsonIgnore]
        //public byte[]? ProfilePicture { get; set; }

        [Required, MinLength(2), MaxLength(200)]
        public string Faculty { get; set; }

        [Required]
        public string SpecialityIDTemp { get; set; }
        public string SubSpecialities { get; set; }
        public string Insurances { get; set; }

        public List<int> GetSubSpecialitiesList()
        {
            if (SubSpecialities is null)
            {
                return null;
            }
            return SubSpecialities.Split(',').Where(n => int.TryParse(n,out int _)).Select(n => int.Parse(n)).ToList();
        }

        public List<int> GetInsurancesList()
        {
            if (Insurances is null )
            {
                return null;   
            }
            return Insurances.Split(',').Where(n => int.TryParse(n, out int _)).Select(n => int.Parse(n)).ToList();
        }
    }

    public class DocRegisterImages
    {
        //[Required]
        public IFormFile? LicenseImg { get; set; }

        //[Required]
        public IFormFile? ProfilePicture { get; set; }
    }

}
