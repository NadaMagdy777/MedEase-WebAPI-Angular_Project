import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppointmentService } from 'src/app/services/appointment/appointment.service';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { IPatientAppointmentDetailsDto } from 'src/app/sharedClassesAndTypes/appointment/i-patient-appointment-details-dto';

@Component({
  selector: 'app-confirmed-appointment',
  templateUrl: './confirmed-appointment.component.html',
  styleUrls: ['./confirmed-appointment.component.css']
})
export class ConfirmedAppointmentComponent {
  constructor(
    private _appointmentService: AppointmentService,
    private _userAuthService: UserAuthService
  ) {}

  confirmedAppointments: IPatientAppointmentDetailsDto[] = [];
  filteredData:IPatientAppointmentDetailsDto[] = [];
  allSubscriptions: Subscription[] = [];
  p:number=1;

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._appointmentService
        .getPateintConfirmedAppointments()
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
    this.filteredData=this.filteredData.filter((Appoinment:IPatientAppointmentDetailsDto)=>{
      return Appoinment.doctorName.toLowerCase().includes(value.toLowerCase()) 
    });
    


  }

}
