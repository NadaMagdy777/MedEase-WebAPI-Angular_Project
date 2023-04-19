using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class InvestigationImage
    {
        public int Id { get; set; }

        [ForeignKey ("Investigation")]
        public int InvestigationId {get; set; }
        public Investigation Investigation { get; set; }
        public string Image { get; set; }           //==> array of bytes
    }
}
