using MedEase.Core.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }     //=> name
        public string LastName { get; set; }
        public string SSN { get; set; }        //==> ignore
        public DateTime BirthDate { get; set; }     //==>Age
        public Gender Gender { get; set; }          //==>get   
        public int Building { get; set; }
        public string Street { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
        public virtual Doctor? Doctor {get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
