import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllDoctorComponent } from 'src/app/components/doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/doctor/doctor-index/doctor-index.component';
import { DoctorDetailsComponent } from 'src/app/components/doctor/doctor-details/doctor-details.component';
import { SearchComponent } from 'src/app/components/doctor/search/search.component';

import { DoctorAppointmentsComponent } from 'src/app/components/doctor/doctor-appointments/doctor-appointments.component';
import { DoctorPendingAppointmentsComponent } from 'src/app/components/doctor/doctor-pending-appointments/doctor-pending-appointments.component';
import { DoctorConfirmedAppointmentsComponent } from 'src/app/components/doctor/doctor-confirmed-appointments/doctor-confirmed-appointments.component';
import { DoctorRegisterComponent } from 'src/app/components/authentication/doctor-register/doctor-register.component';
import { EditProfileComponent } from 'src/app/components/doctor/edit-profile/edit-profile.component';
const routes: Routes = [
  {
    path: ':speciality/:city/:region/:name',
    component: AllDoctorComponent,
    children: [{ path: 'All', component: DoctorIndexComponent }],
  },
  { path: 'details/:id', component: DoctorDetailsComponent },
  {
    path: 'Appointment',
    component: DoctorAppointmentsComponent,
    children: [
      // { path: '', redirectTo: '/Pending', pathMatch: 'full' },
      { path: 'Pending', component: DoctorPendingAppointmentsComponent },
      { path: 'Confirmed', component: DoctorConfirmedAppointmentsComponent },
    ],
  },
  {
    path:'profile',component: EditProfileComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DoctorRoutingModule {}
