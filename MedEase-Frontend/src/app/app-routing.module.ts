import { LoginComponent } from './components/authentication/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Authentication/auth.guard';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { HomeComponent } from './components/home/home/home.component';
import { ReserveAppointementComponent } from './components/Doctor/reserve-appointement/reserve-appointement.component';
import { PatientRegisterComponent } from './components/authentication/patient-register/patient-register.component';
import { DoctorAuthGuard } from './guards/doctor-auth.guard';
import { PatientAuthGuard } from './guards/patient-auth.guard';

import { HistoryComponent } from './components/patient/history/history.component';

const routes: Routes = [
  { path: '', redirectTo: '/Home', pathMatch: 'full' },
  {path:"Home",component:HomeComponent},
  {path:'login', component: LoginComponent},
  {path:'register', component: PatientRegisterComponent},
  {path:"doctor",loadChildren:()=>import('./modules/Doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)},
  {path:'account',loadChildren:()=>import('./modules/patient/patient.module').then(mod=>mod.PatientModule)},
  {path:'userAppointments',component:AppointmentsComponent},
  {path:'userInsurance',component:InsuranceComponent},
  {path:'history/:id',component:HistoryComponent},

  { path: '**', component: HomeComponent},

  {path:'reserve-appointement',component:ReserveAppointementComponent},
  { path: '**', component: HomeComponent},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
