import { PatientQuestionsComponent } from './../../components/patient/questions/patient-questions/patient-questions.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicalHistoryComponent } from 'src/app/components/patient/medical-history/medical-history.component';
import { AskQuestionComponent } from 'src/app/components/patient/ask-question/ask-question.component';
import { ProfileComponent } from 'src/app/components/patient/profile/profile.component';
import { AnsweredQuestionsComponent } from 'src/app/components/patient/questions/answered/answered-questions/answered-questions.component';
import { UnansweredQuestionsComponent } from 'src/app/components/patient/questions/unanswered/unanswered-questions/unanswered-questions.component';
import { AppointmentsComponent } from 'src/app/components/patient/appointments/appointments.component';
import { PenddingAppointmentComponent } from 'src/app/components/patient/pendding-appointment/pendding-appointment.component';
import { ConfirmedAppointmentComponent } from 'src/app/components/patient/confirmed-appointment/confirmed-appointment.component';
import { ReviewComponent } from 'src/app/components/patient/review/review.component';
import { ShowDiagnosisComponent } from 'src/app/components/patient/show-diagnosis/show-diagnosis.component';


const routes: Routes = [
  { path:'profile',component:ProfileComponent},
  { path:'medicalHistory',component:MedicalHistoryComponent},
  { path:'ask',component:AskQuestionComponent},
  { path:'Review/:id',component:ReviewComponent},
  { path:'questions',component:PatientQuestionsComponent, children:[
    { path:'answered',component:AnsweredQuestionsComponent},
    { path:'unanswered',component:UnansweredQuestionsComponent},
  ]},
  {
    path:'diagnosis/:appointmentId/:doctorName',component:ShowDiagnosisComponent
  },
  {
    path: 'Appointment',
    component: AppointmentsComponent,
    children: [
      // { path: '', redirectTo: '/Pending', pathMatch: 'full' },
      { path: 'Pending', component: PenddingAppointmentComponent },
      { path: 'Confirmed', component: ConfirmedAppointmentComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientRoutingModule { 

}
