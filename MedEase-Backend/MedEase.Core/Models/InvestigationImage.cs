using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class InvestigationImage
    {
        public int Id { get; set; }
        public int InvestigationId {get; set; }
        public Investigation Investigation { get; set; }
        public byte[] Image { get; set; }

    }
}
