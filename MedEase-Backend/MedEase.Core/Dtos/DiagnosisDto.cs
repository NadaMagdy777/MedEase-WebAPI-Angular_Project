using MedEase.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class DiagnosisDto
    {
        public string Details { get; set; }
        public int ExaminationID { get; set; }
    }
}
