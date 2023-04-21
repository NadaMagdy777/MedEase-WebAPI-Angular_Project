import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FooterComponent } from './components/layout/footer/footer.component';
import { AuthGuard } from './Authentication/auth.guard';

const routes: Routes = [
  {path:"home",component:FooterComponent,canActivate:[AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
