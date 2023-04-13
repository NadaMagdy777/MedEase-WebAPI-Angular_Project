using MedEase.Core.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }     //=> name
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string SSN { get; set; }        //==> ignore
        public DateTime BirthDate { get; set; }     //==>Age
        public Gender Gender { get; set; }          //==>get   
        public virtual Address Address { get; set; }
        public virtual Doctor? Doctor {get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
