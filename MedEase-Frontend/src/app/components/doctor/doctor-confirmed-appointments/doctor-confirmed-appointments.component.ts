import { Subscription } from 'rxjs';
import { IDoctorPendingAppointmentDetailsDto } from './../../../sharedClassesAndTypes/appointment/i-doctor-pending-appointment-details-dto';
import { AppointmentService } from './../../../services/appointment/appointment.service';
import { Component, OnInit } from '@angular/core';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { IDoctorConfirmedAppointmentDetailsDto } from 'src/app/sharedClassesAndTypes/appointment/i-doctor-confirmed-appointment-details-dto';

@Component({
  selector: 'app-doctor-confirmed-appointments',
  templateUrl: './doctor-confirmed-appointments.component.html',
  styleUrls: ['./doctor-confirmed-appointments.component.css']
})
export class DoctorConfirmedAppointmentsComponent {
  constructor(
    private _appointmentService: AppointmentService,
    private _userAuthService: UserAuthService
  ) {}

  confirmedAppointments: IDoctorConfirmedAppointmentDetailsDto[] = [];
  filteredData:IDoctorConfirmedAppointmentDetailsDto[] = [];
  allSubscriptions: Subscription[] = [];
  p:number=1;

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._appointmentService
        .getDoctorConfirmedAppointments()
        .subscribe((res) => {
          if (res.success) {
            this.confirmedAppointments = res.data;
            this.filteredData= this.confirmedAppointments
            console.log(this.confirmedAppointments);
          } else {
            console.log(res.message); 
          }
        })
    );
 
  
  

  }
  SearchData(value:string){
    this.filteredData=this.confirmedAppointments
    this.filteredData=this.filteredData.filter((Appoinment:IDoctorConfirmedAppointmentDetailsDto)=>{
      return Appoinment.patientName.toLowerCase().includes(value.toLowerCase()) 
    });
    


  }


}
