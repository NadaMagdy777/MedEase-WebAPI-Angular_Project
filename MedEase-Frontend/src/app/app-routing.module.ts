import { LoginComponent } from './components/authentication/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Authentication/auth.guard';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { HomeComponent } from './components/home/home/home.component';
import { PatientRegisterComponent } from './components/authentication/patient-register/patient-register.component';



const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {path:"home",component:HomeComponent,canActivate:[AuthGuard]},
  {path:'login', component: LoginComponent},
  {path:'register', component: PatientRegisterComponent},
  {path:"doctor",loadChildren:()=>import('./modules/Doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)},
  {path:'account',loadChildren:()=>import('./modules/patient/patient.module').then(mod=>mod.PatientModule)},
  {path:'userAppointments',component:AppointmentsComponent},
  {path:'userInsurance',component:InsuranceComponent},




      { path: '**', component: HomeComponent},


    ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
