import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllDoctorComponent } from 'src/app/components/Doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/Doctor/doctor-index/doctor-index.component';
import { DoctorDetailsComponent } from 'src/app/components/Doctor/doctor-details/doctor-details.component';
import { SearchComponent } from 'src/app/components/Doctor/search/search.component';

const routes: Routes = [
  
  {
    path:':speciality/:city/:region/:name',
    component:AllDoctorComponent,
    children:[
      {path:'All',component:DoctorIndexComponent},
    ]
  },
  {path:'details/:id',component:DoctorDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoctorRoutingModule { }
