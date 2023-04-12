using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Models
{
    public class Certificates
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Issuer { get; set; }
        public DateTime IssueDate { get; set; }

        [ForeignKey ("Doctor")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
