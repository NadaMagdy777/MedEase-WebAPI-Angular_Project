using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class DoctorSubspeciality
    {
        [Key, Column(Order = 0)]
        [ForeignKey("doctor")]
        public int DocID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("subSpeciality")]
        public int  SubspecID { get; set; }

       public virtual Doctor doctor { get; set; }
       public virtual subSpeciality subSpeciality { get; set; }


    }
}
