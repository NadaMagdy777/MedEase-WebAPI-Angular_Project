import { LoginComponent } from './components/authentication/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { HomeComponent } from './components/home/home/home.component';
import { ReserveAppointementComponent } from './components/doctor/reserve-appointement/reserve-appointement.component';
import { PatientRegisterComponent } from './components/authentication/patient-register/patient-register.component';
import { ExaminationInfoComponent } from './components/patient/examination-info/examination-info.component';
const routes: Routes = [
  { path: '', redirectTo: '/Home', pathMatch: 'full' },
  {path:"Home",component:HomeComponent},
  {path:'login', component: LoginComponent},
  {path:'register', component: PatientRegisterComponent},
  {path:"doctor",loadChildren:()=>import('./modules/doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)},
  {path:'account',loadChildren:()=>import('./modules/patient/patient.module').then(mod=>mod.PatientModule)},
  {path:'userAppointments',component:AppointmentsComponent},
  {path:'userInsurance',component:InsuranceComponent},
  {path:'history/:id',component:ExaminationInfoComponent},

  
  {path:'reserve-appointement',component:ReserveAppointementComponent},
  { path: '**', component: HomeComponent},
    ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
