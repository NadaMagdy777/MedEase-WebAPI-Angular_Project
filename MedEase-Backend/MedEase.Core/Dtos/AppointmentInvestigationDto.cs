using System.ComponentModel.DataAnnotations;

namespace MedEase.Core.Dtos
{
    public class AppointmentInvestigationDto
    {
        [Required]
        public int AppointmentId { get; set; }
        
        [Required]
        public string Description { get; set; }
        public bool HasImage { get; set; }
        public string? Image { get; set; }           //==> array of bytes
    }
}