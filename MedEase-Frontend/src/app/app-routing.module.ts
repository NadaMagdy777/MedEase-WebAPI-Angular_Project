import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterComponent } from './components/layout/footer/footer.component';
import { AuthGuard } from './Authentication/auth.guard';
import { ProfileComponent } from './components/Patient/profile/profile.component';
import { AppointmentsComponent } from './components/Patient/appointments/appointments.component';
import { InsuranceComponent } from './components/Patient/insurance/insurance.component';

const routes: Routes = [
  {path:"home",component:FooterComponent,canActivate:[AuthGuard]},
  {path:"doctor",loadChildren:()=>import('./modules/Doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)},
  {path:'accountProfile',component:ProfileComponent},
  {path:'userAppointments',component:AppointmentsComponent},
  {path:'userInsurance',component:InsuranceComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
