import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { HomeComponent } from './components/home/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CarouselComponent } from './components/home/carousel/carousel.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { ProfileComponent } from './components/patient/profile/profile.component';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { AppointementsComponent } from './components/doctor/appointements/appointements.component';

import { HomeBannerComponent } from './components/home/home/home-banner/home-banner.component';

import { DoctorAppointmentsComponent } from 'src/app/components/doctor/doctor-appointments/doctor-appointments.component';
import { DoctorPendingAppointmentsComponent } from 'src/app/components/doctor/doctor-pending-appointments/doctor-pending-appointments.component';
import { DoctorConfirmedAppointmentsComponent } from 'src/app/components/doctor/doctor-confirmed-appointments/doctor-confirmed-appointments.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    CarouselComponent,
    AppointmentsComponent,
    InsuranceComponent,

   

    HomeBannerComponent,

    DoctorAppointmentsComponent,
    DoctorPendingAppointmentsComponent,
    DoctorConfirmedAppointmentsComponent,
      
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CarouselModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
