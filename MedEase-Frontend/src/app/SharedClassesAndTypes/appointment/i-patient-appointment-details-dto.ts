import { AppointmentStatus } from "../enums/appointment-status";

export interface IPatientAppointmentDetailsDto {
    appointmentID:number,
    dateTime :any ,  
    status:AppointmentStatus 
    doctorName:string
    doctorSpeciality:string
    reviewd:boolean
    prescriptiotion:any,
    diagnosisDetails:string
}
