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

import { HomeBannerComponent } from './components/home/home/home-banner/home-banner.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { PatientRegisterComponent } from './components/authentication/patient-register/patient-register.component';
import { NavUserNamePipe } from './pipes/nav-user-name.pipe';
import { DoctorRegisterComponent } from './components/authentication/doctor-register/doctor-register.component';
import { HistoryComponent } from './components/patient/history/history.component';
import { AnsweredQuestionsComponent } from './components/patient/questions/answered/answered-questions/answered-questions.component';
import { UnansweredQuestionsComponent } from './components/patient/questions/unanswered/unanswered-questions/unanswered-questions.component';
import { PatientQuestionsComponent } from './components/patient/questions/patient-questions/patient-questions.component';
import { ExaminationInfoComponent } from './components/patient/examination-info/examination-info.component';
import { DoctorAnswerdQuestionsComponent } from './components/patient/questions/doctor/doctor-answerd-questions/doctor-answerd-questions.component';
import { DoctorUnAnswerdComponent } from './components/patient/questions/doctor/doctor-un-answerd/doctor-un-answerd.component';
import { AnswerQuestionComponent } from './components/patient/questions/doctor/answer-question/answer-question.component';
import { QuestionsLayoutComponent } from './components/patient/questions/doctor/questions-layout/questions-layout.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    CarouselComponent,
    HomeBannerComponent,
    LoginComponent,
    PatientRegisterComponent,
    NavUserNamePipe,
    DoctorRegisterComponent,
    HistoryComponent,
    AnsweredQuestionsComponent,
    UnansweredQuestionsComponent,
    PatientQuestionsComponent,
    
    LoginComponent,
    HistoryComponent,
    ExaminationInfoComponent,
    DoctorAnswerdQuestionsComponent,
    DoctorUnAnswerdComponent,
    AnswerQuestionComponent,
    QuestionsLayoutComponent,
  
      
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
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
