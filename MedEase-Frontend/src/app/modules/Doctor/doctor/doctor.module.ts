import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DoctorRoutingModule } from './doctor-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { AllDoctorComponent } from 'src/app/components/doctor/all-doctor/all-doctor.component';
import { DoctorIndexComponent } from 'src/app/components/doctor/doctor-index/doctor-index.component';
import { DoctorAppointmentsComponent } from 'src/app/components/doctor/doctor-appointments/doctor-appointments.component';
import { DoctorPendingAppointmentsComponent } from 'src/app/components/doctor/doctor-pending-appointments/doctor-pending-appointments.component';
import { DoctorConfirmedAppointmentsComponent } from 'src/app/components/doctor/doctor-confirmed-appointments/doctor-confirmed-appointments.component';
import { MinutesToTimePipe } from 'src/app/pipes/minutes-to-time.pipe';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SearchComponent } from 'src/app/components/doctor/search/search.component';
import { DoctorDetailsComponent } from 'src/app/components/doctor/doctor-details/doctor-details.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/interceptors/token.interceptor';
import { EditProfileComponent } from 'src/app/components/doctor/edit-profile/edit-profile.component';
import { AppointementsComponent } from 'src/app/components/doctor/doctorAppointements/appointements.component';
import { ReserveAppointementComponent } from 'src/app/components/Doctor/reserve-appointement/reserve-appointement.component';
import { DoctorScheduleComponent } from 'src/app/components/doctor/doctor-schedule/doctor-schedule.component';

@NgModule({
  declarations: [
    DoctorIndexComponent,
    DoctorAppointmentsComponent,
    DoctorPendingAppointmentsComponent,
    DoctorConfirmedAppointmentsComponent,
    AllDoctorComponent,
    MinutesToTimePipe,
    SearchComponent,
    DoctorDetailsComponent,
    AppointementsComponent,
    ReserveAppointementComponent,
    DoctorDetailsComponent,
    EditProfileComponent,
    DoctorScheduleComponent
  ],
  imports: [
    CommonModule,
    DoctorRoutingModule,
    NgxPaginationModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,

  ],
  providers:[{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }]
})
export class DoctorModule {}
