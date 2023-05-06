import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AppointmentService } from 'src/app/services/appointment/appointment.service';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { IPatientAppointmentDetailsDto } from 'src/app/sharedClassesAndTypes/appointment/i-patient-appointment-details-dto';

@Component({
  selector: 'app-show-diagnosis',
  templateUrl: './show-diagnosis.component.html',
  styleUrls: ['./show-diagnosis.component.css']
})
export class ShowDiagnosisComponent {


  doctorName:string=""
  description:string=""
  appointmentId:number=0
  allSubscriptions: Subscription[] = [];
  


  constructor(private router:Router ,private route:ActivatedRoute,private _appointmentService: AppointmentService,){

  }

  ngOnInit(): void {
    this.appointmentId= this.route.snapshot.params['appointmentId']
    this.doctorName= this.route.snapshot.params['doctorName']

    this.allSubscriptions.push(
      this._appointmentService
        .getPateintConfirmedAppointments()
        .subscribe((res) => {
          if (res.success) {
            this.description = res.data.find((details:IPatientAppointmentDetailsDto)=>{
              if(details.appointmentID==this.appointmentId){
                return details.diagnosisDetails
              }
              return ""
            });
            console.log(this.description);
          } else {
            console.log(res.message); 
          }
        })
    );
  }
}



