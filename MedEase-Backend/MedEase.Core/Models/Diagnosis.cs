using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Diagnosis
    {
        public int ID { get; set; }
        public string Details { get; set; }

        [ForeignKey("Examination")]
        public int ExaminationID { get; set; }
        public virtual Examination Examination { get; set; }
    }
}
