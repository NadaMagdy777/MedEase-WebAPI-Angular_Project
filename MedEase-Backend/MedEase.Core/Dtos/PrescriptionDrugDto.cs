using MedEase.Core.Consts;
using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class PrescriptionDrugDto
    {
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        public int? DrugID { get; set; }
        public int ExaminationID { get; set; }
    }
}
