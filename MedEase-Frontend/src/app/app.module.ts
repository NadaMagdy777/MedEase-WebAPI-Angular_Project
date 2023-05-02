import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { HomeComponent } from './components/home/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CarouselComponent } from './components/home/carousel/carousel.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { AppointmentsComponent } from './components/patient/appointments/appointments.component';
import { InsuranceComponent } from './components/patient/insurance/insurance.component';
import { AppointementsComponent } from './components/doctor/appointements/appointements.component';

import { HomeBannerComponent } from './components/home/home/home-banner/home-banner.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { PatientRegisterComponent } from './components/authentication/patient-register/patient-register.component';
import { DoctorRegisterComponent } from './components/authentication/doctor-register/doctor-register.component';


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
    LoginComponent,
    PatientRegisterComponent,
    DoctorRegisterComponent
      
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
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent],
})
export class AppModule {}
