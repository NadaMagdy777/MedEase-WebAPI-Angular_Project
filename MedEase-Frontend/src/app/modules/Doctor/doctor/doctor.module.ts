import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorRoutingModule } from './doctor-routing.module';
import {NgxPaginationModule} from 'ngx-pagination';
import { AllDoctorComponent } from 'src/app/components/Doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/Doctor/doctor-index/doctor-index.component';




@NgModule({
  declarations: [
    DoctorIndexComponent,
    AllDoctorComponent

  ],
  imports: [
    CommonModule,
    DoctorRoutingModule,
    NgxPaginationModule
  ]
})
export class DoctorModule { }
