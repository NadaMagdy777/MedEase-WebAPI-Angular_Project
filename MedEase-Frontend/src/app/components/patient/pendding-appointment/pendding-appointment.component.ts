import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AppointmentService } from 'src/app/services/appointment/appointment.service';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { IAppointmentActionDto } from 'src/app/sharedClassesAndTypes/appointment/i-appointment-action-dto';
import { IPatientAppointmentDetailsDto } from 'src/app/sharedClassesAndTypes/appointment/i-patient-appointment-details-dto';

@Component({
  selector: 'app-pendding-appointment',
  templateUrl: './pendding-appointment.component.html',
  styleUrls: ['./pendding-appointment.component.css']
})
export class PenddingAppointmentComponent {
  constructor(
    private _appointmentService: AppointmentService,
    private _userAuthService: UserAuthService,private router:Router
  ) {}

  pendingAppointments: IPatientAppointmentDetailsDto[] = [];
  filteredData: IPatientAppointmentDetailsDto[] = [];
  allSubscriptions: Subscription[] = [];
  date=new Date().getTime()
  p:number=1;

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._appointmentService
        .getPateintPendingAppointments()
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
    console.log(new Date())
    return new Date(Appoinment.date).valueOf() <= new Date().valueOf()  && Appoinment.status !=4   
  }
  confirmAppointment(AppoinmentId:number){
    let confirmDto:IAppointmentActionDto=new IAppointmentActionDto(AppoinmentId,true)
    this._appointmentService.confirmPatientAppointment(confirmDto).subscribe((res) => {
      if (res.success) {
        console.log("success");
      } else {
        console.log(res.message); 
      }
    })
    //Review
    this.router.navigate(['/account/Review',confirmDto.appointmentID])
  }
  cancelAppointment(AppoinmentId:number){
    let cancelmDto:IAppointmentActionDto=new IAppointmentActionDto(AppoinmentId,false)
    this._appointmentService.confirmPatientAppointment(cancelmDto).subscribe((res) => {
      if (res.success) {
        console.log("success");
        this.router.navigate(['/account/Appointment'])

      } else {
        console.log(res.message); 
      }
    })

  }
  SearchData(value:string){
    this.filteredData=this.pendingAppointments
    this.filteredData=this.filteredData.filter((Appoinment:IPatientAppointmentDetailsDto)=>{
      return Appoinment.doctorName.toLowerCase().includes(value.toLowerCase()) 
    });
    


  }


}
