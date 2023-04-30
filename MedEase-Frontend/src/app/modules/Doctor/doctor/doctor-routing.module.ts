import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllDoctorComponent } from 'src/app/components/Doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/Doctor/doctor-index/doctor-index.component';
import { DoctorDetailsComponent } from 'src/app/components/Doctor/doctor-details/doctor-details.component';
import { SearchComponent } from 'src/app/components/Doctor/search/search.component';

import { DoctorAppointmentsComponent } from 'src/app/components/doctor/doctor-appointments/doctor-appointments.component';
import { DoctorPendingAppointmentsComponent } from 'src/app/components/doctor/doctor-pending-appointments/doctor-pending-appointments.component';
import { DoctorConfirmedAppointmentsComponent } from 'src/app/components/doctor/doctor-confirmed-appointments/doctor-confirmed-appointments.component';
const routes: Routes = [
  {
    path: ':speciality/:city/:region/:name',
    component: AllDoctorComponent,
    children: [
      { path: 'All', component: DoctorIndexComponent },
      // { path: 'Appointment', component: DoctorAppointmentsComponent, children: [
      //   { path: '',  redirectTo: '/Pending', pathMatch: 'full' },
      //   { path: 'Pending', component: DoctorPendingAppointmentsComponent },
      //   { path: 'Confirmed', component: DoctorConfirmedAppointmentsComponent },
      // ] },
    ],
  },
  { path: 'details', component: DoctorDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DoctorRoutingModule {}
