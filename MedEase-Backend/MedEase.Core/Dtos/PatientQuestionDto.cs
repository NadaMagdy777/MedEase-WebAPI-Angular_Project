namespace MedEase.Core.Dtos
{
    public class PatientQuestionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int SpecialityId { get; set; }
    }
}
