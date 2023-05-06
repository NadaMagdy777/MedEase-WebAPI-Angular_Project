import { Subscription } from 'rxjs';
import { IDoctorPendingAppointmentDetailsDto } from './../../../sharedClassesAndTypes/appointment/i-doctor-pending-appointment-details-dto';
import { AppointmentService } from './../../../services/appointment/appointment.service';
import { Component, OnInit } from '@angular/core';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { IAppointmentActionDto } from 'src/app/sharedClassesAndTypes/appointment/i-appointment-action-dto';

@Component({
  selector: 'app-doctor-pending-appointments',
  templateUrl: './doctor-pending-appointments.component.html',
  styleUrls: ['./doctor-pending-appointments.component.css'],
})
export class DoctorPendingAppointmentsComponent implements OnInit {
  constructor(
    private _appointmentService: AppointmentService,
    private _userAuthService: UserAuthService
  ) {}

  pendingAppointments: IDoctorPendingAppointmentDetailsDto[] = [];
  filteredData: IDoctorPendingAppointmentDetailsDto[] = [];
  allSubscriptions: Subscription[] = [];
  date=new Date()
  p:number=1;

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._appointmentService
        .getDoctorPendingAppointments()
        .subscribe((res) => {
          if (res.success) {
            this.pendingAppointments = res.data;
            this.filteredData=this.pendingAppointments
            console.log(this.pendingAppointments);
          } else {
            console.log(res.message); 
          }
        })
    );
 
  
  

  }

  ConfirmStatus(Appoinment:any){
    
    return (new Date(Appoinment.date).valueOf() <= new Date(this.date).valueOf()) 
     
  }
  confirmAppointment(AppoinmentId:number){
    let confirmDto:IAppointmentActionDto=new IAppointmentActionDto(AppoinmentId,true)
    this._appointmentService.confirmDoctorAppointment(confirmDto).subscribe((res) => {
      if (res.success) {
        console.log("success");
      } else {
        console.log(res.message); 
      }
    })
  }
  cancelAppointment(AppoinmentId:number){
    let cancelmDto:IAppointmentActionDto=new IAppointmentActionDto(AppoinmentId,false)
    this._appointmentService.confirmDoctorAppointment(cancelmDto).subscribe((res) => {
      if (res.success) {
        console.log("success");
      } else {
        console.log(res.message); 
      }
    })
  }
  SearchData(value:string){
    this.filteredData=this.pendingAppointments
    this.filteredData=this.filteredData.filter((Appoinment:IDoctorPendingAppointmentDetailsDto)=>{
      return Appoinment.patientName.toLowerCase().includes(value.toLowerCase()) 
    });
    


  }

}
