import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentsComponent } from 'src/app/components/patient/appointments/appointments.component';

import { PatientRoutingModule } from './patient-routing.module';
import { ProfileComponent } from 'src/app/components/patient/profile/profile.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/interceptors/token.interceptor';
import { MedicalHistoryComponent } from 'src/app/components/patient/medical-history/medical-history.component';
import { AskQuestionComponent } from 'src/app/components/patient/ask-question/ask-question.component';
import { ConfirmedAppointmentComponent } from 'src/app/components/patient/confirmed-appointment/confirmed-appointment.component';
import { PenddingAppointmentComponent } from 'src/app/components/patient/pendding-appointment/pendding-appointment.component';
import { ReviewComponent } from 'src/app/components/patient/review/review.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { InsuranceComponent } from 'src/app/components/patient/insurance/insurance.component';

@NgModule({
  declarations: [
    ProfileComponent,
    MedicalHistoryComponent,
    InsuranceComponent,
    AskQuestionComponent,
    AppointmentsComponent,
    ConfirmedAppointmentComponent,
    PenddingAppointmentComponent,
    ReviewComponent,

  ],
  imports: [
    CommonModule,
    PatientRoutingModule,
    ReactiveFormsModule,
    RouterModule,
    NgxPaginationModule
  ],
  providers:[{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }]
})
export class PatientModule { }
