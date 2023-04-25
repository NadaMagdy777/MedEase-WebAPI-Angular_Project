import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterComponent } from './components/layout/footer/footer.component';
import { AuthGuard } from './Authentication/auth.guard';

const routes: Routes = [
  {path:"home",component:FooterComponent,canActivate:[AuthGuard]},
  {path:"doctor",loadChildren:()=>import('./modules/Doctor/doctor/doctor.module').then(mod=>mod.DoctorModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
