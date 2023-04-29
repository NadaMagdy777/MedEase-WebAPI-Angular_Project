import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorRoutingModule } from './doctor-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { AllDoctorComponent } from 'src/app/components/Doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/Doctor/doctor-index/doctor-index.component';
import { DoctorAppointmentsComponent } from 'src/app/components/doctor/doctor-appointments/doctor-appointments.component';
import { DoctorPendingAppointmentsComponent } from 'src/app/components/doctor/doctor-pending-appointments/doctor-pending-appointments.component';
import { DoctorConfirmedAppointmentsComponent } from 'src/app/components/doctor/doctor-confirmed-appointments/doctor-confirmed-appointments.component';

@NgModule({
  declarations: [
    DoctorIndexComponent,
    AllDoctorComponent,
    DoctorAppointmentsComponent,
    DoctorPendingAppointmentsComponent,
    DoctorConfirmedAppointmentsComponent,
  ],
  imports: [CommonModule, DoctorRoutingModule, NgxPaginationModule],
})
export class DoctorModule {}
