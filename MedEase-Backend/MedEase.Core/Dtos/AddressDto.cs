using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class AddressDto
    {
        public int Building { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
    }
}
