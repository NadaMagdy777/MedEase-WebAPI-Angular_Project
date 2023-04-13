using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class PrescriptionDrug
    {
        public int Quantity { get; set; }
        public string? Notes { get; set; }

        [Column(Order = 0), ForeignKey("Drug")]
        public int DrugID { get; set; }
        public Drug Drug { get; set; }

        [Column(Order = 1), ForeignKey("Examination")]
        public int ExaminationID { get; set; }
        public virtual Examination Examination { get; set; }
    }
}
