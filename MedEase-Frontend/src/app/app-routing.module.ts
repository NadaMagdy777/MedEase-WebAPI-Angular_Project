import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Authentication/auth.guard';
import { ProfileComponent } from './components/patient/profile/profile.component';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { HomeComponent } from './components/home/home/home.component';

const routes: Routes = [
  {path:"home",component:HomeComponent,canActivate:[AuthGuard]},
  {path:"doctor",loadChildren:()=>import('./modules/Doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)},
  {path:'accountProfile',component:ProfileComponent},
  {path:'userAppointments',component:AppointmentsComponent},
  {path:'userInsurance',component:InsuranceComponent},



  {path:"**",component:HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
