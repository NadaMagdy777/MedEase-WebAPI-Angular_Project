import { AppointmentStatus } from "../enums/appointment-status";

export interface IPatientAppointmentDetailsDto {
    appointmentID:number,
    date :any ,  
    status:AppointmentStatus 
    doctorName:string
    doctorSpeciality:string
    reviewd:boolean
    prescriptiotion:any,
    diagnosisDetails:string
}
