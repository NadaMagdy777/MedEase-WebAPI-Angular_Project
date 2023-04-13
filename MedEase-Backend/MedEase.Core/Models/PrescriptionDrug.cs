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
        public int ID { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }

        [ForeignKey ("Drug")]
        public int DrugID { get; set; }
        public Drug Drug { get; set; }

        [ForeignKey("Examination")]
        public int ExaminationID { get; set; }
        public virtual Examination Examination { get; set; }
    }
}
