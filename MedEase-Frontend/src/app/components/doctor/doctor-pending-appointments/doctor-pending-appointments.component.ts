import { IDoctorPendingAppointmentDetailsDto } from './../../../SharedClassesAndTypes/appointment/i-doctor-pending-appointment-details-dto';
import { AppointmentService } from './../../../services/appointment/appointment.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doctor-pending-appointments',
  templateUrl: './doctor-pending-appointments.component.html',
  styleUrls: ['./doctor-pending-appointments.component.css'],
})
export class DoctorPendingAppointmentsComponent implements OnInit {
  constructor(private _appointmentService: AppointmentService) {}

  pendingAppointments: IDoctorPendingAppointmentDetailsDto[] = [];


  
  ngOnInit(): void {
    this._appointmentService.getDoctorPendingAppointments().subscribe((res) => {
      if (res.success) {
        this.pendingAppointments = res.data;
        console.log(this.pendingAppointments);
      } else {
        console.log(res.message); /////Bootstrap Error Message
      }
    });
  }
}
