import { IDiagnosisDto } from "../diagnosis/i-diagnosis-dto";
import { AppointmentStatus } from "../enums/appointment-status";
import { IAppointmentInvestigationDto } from "../investigation/i-appointment-investigation-dto";
import { IPatientMedicalHistoryDto } from "../patient/i-patient-medical-history-dto";
import { Gender } from 'src/app/SharedClassesAndTypes/enums/gender';


export interface IDoctorConfirmedAppointmentDetailsDto {
    appointmentID: number;
    date: Date;
    status: AppointmentStatus;
    patientID: number;
    patientName: string;
    patientPhone: string;
    patientBirthDate: Date;
    patientGender: Gender;
    diagnosis: IDiagnosisDto;
    
}
