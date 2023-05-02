import { Subscription } from 'rxjs';
import { IDoctorPendingAppointmentDetailsDto } from './../../../sharedClassesAndTypes/appointment/i-doctor-pending-appointment-details-dto';
import { AppointmentService } from './../../../services/appointment/appointment.service';
import { Component, OnInit } from '@angular/core';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';

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
  allSubscriptions: Subscription[] = [];

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._appointmentService
        .getDoctorPendingAppointments()
        .subscribe((res) => {
          if (res.success) {
            this.pendingAppointments = res.data;
            console.log(this.pendingAppointments);
          } else {
            console.log(res.message); /////Bootstrap Error Message
          }
        })
    );

      /////
  //For Test Only (Remove Later)
  ////
    // this._userAuthService.login("user@example.com","123abcABC@");
  /////
  //For Test Only (Remove Later)
  ////

  }
}
