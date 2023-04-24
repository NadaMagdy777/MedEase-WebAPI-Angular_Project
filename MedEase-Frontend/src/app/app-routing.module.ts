import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterComponent } from './components/layout/footer/footer.component';
import { AuthGuard } from './Authentication/auth.guard';
import { AllDoctorComponent } from './components/Doctor/all-doctor/all-doctor.component';

const routes: Routes = [
  {path:"home",component:FooterComponent,canActivate:[AuthGuard]},
  {
    path:'Doctors',
    component:AllDoctorComponent,
    children:[
    ]
   },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
