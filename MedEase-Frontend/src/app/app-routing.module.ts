import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Authentication/auth.guard';
import { ProfileComponent } from './components/patient/profile/profile.component';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { HomeComponent } from './components/home/home/home.component';

import { DoctorAppointmentsComponent } from 'src/app/components/doctor/doctor-appointments/doctor-appointments.component';
import { DoctorPendingAppointmentsComponent } from 'src/app/components/doctor/doctor-pending-appointments/doctor-pending-appointments.component';
import { DoctorConfirmedAppointmentsComponent } from 'src/app/components/doctor/doctor-confirmed-appointments/doctor-confirmed-appointments.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {path:"home",component:HomeComponent,canActivate:[AuthGuard]},
  //{path:"doctor",loadChildren:()=>import('./modules/Doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)},
  {path:'accountProfile',component:ProfileComponent},
  {path:'userAppointments',component:AppointmentsComponent},
  {path:'userInsurance',component:InsuranceComponent},



  //{path:"**",component:HomeComponent},    ///=======> Not Found Component
      // { path: '**', component: DoctorAppointmentsComponent, children: [
      //   { path: '',  redirectTo: '/Pending', pathMatch: 'full' },
      //   { path: 'Pending', component: DoctorPendingAppointmentsComponent },
      //   { path: 'Confirmed', component: DoctorConfirmedAppointmentsComponent },
      // ] },

      { path: '**', component: DoctorPendingAppointmentsComponent },
      // { path: '**', component: DoctorConfirmedAppointmentsComponent },


    ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
