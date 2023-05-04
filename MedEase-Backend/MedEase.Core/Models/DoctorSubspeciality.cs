using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class DoctorSubspeciality
    {
        [Column(Order = 0)]
        [ForeignKey("Doctor")]
        public int DocID { get; set; }

        [Column(Order = 1)]
        [ForeignKey("SubSpeciality")]
        public int SubSpecialityID { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual SubSpeciality SubSpeciality { get; set; }


    }
}
