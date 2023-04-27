import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllDoctorComponent } from 'src/app/components/Doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/Doctor/doctor-index/doctor-index.component';
import { DoctorDetailsComponent } from 'src/app/components/Doctor/doctor-details/doctor-details.component';
const routes: Routes = [
  
  {
    path:'',
    component:AllDoctorComponent,
    children:[
      {path:'All',component:DoctorIndexComponent},
    ]
   },
   {path:'details',component:DoctorDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoctorRoutingModule { }
