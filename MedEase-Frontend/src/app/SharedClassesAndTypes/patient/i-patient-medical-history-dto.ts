export interface IPatientMedicalHistoryDto {
  hasAllergies: boolean;
  hadSurgery: boolean;
  hasChronicIllnesses: boolean;
  takeMedications: boolean;
  hasHospitalized: boolean;
  isSmoking: boolean;
}

// public class PatientMedicalHistoryDto
// {
//     [Required]
//     public bool HasAllergies { get; set; }
//     [Required]
//     public bool HadSurgery { get; set; }
//     [Required]
//     public bool HasChronicIllnesses { get; set; }
//     [Required]
//     public bool TakeMedications { get; set; }
//     [Required]
//     public bool HasHospitalized { get; set; }
//     [Required]
//     public bool IsSmoking { get; set; }
// }
