using MedEase.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public interface IUserRegisterDto
    {
        string Email { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string SSN { get; set; }
        string Phone { get; set; }
        Gender Gender { get; set; }
        DateTime BirthDate { get; set; }
        int Building { get; set; }
        string Street { get; set; }
        int AddressID { get; set; }
    }
}
