export interface IAppointmentInvestigationDto {
  appointmentId: number;
  description: string;
  hasImage: boolean;
  image: string; //=====> Array of bytes
}

// [Required]
// public int AppointmentId { get; set; }

// [Required]
// public string Description { get; set; }
// public bool HasImage { get; set; }
// public string? Image { get; set; }
