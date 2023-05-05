import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicalHistoryComponent } from 'src/app/components/patient/medical-history/medical-history.component';
import { ProfileComponent } from 'src/app/components/patient/profile/profile.component';

const routes: Routes = [
  { path:'profile',component:ProfileComponent},
  { path:'medicalHistory',component:MedicalHistoryComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientRoutingModule { }
