using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEase.Core.Dtos
{
    public class CertificateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Issuer { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
