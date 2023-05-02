using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class SubspecialityDto
    {
        public int ID { get; set; }

        [Required, MinLength(4), MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int SepcID { get; set; }


    }
}
