import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { HomeComponent } from './components/home/home/home.component';
import { BrowserAnimationsModule  } from "@angular/platform-browser/animations";
import { CarouselComponent } from './components/home/carousel/carousel.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { SearchComponent } from './components/home/search/search.component';
import { DoctorDetailsComponent } from './components/Doctor/doctor-details/doctor-details.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    CarouselComponent,
    SearchComponent,
    DoctorDetailsComponent,
   

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CarouselModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
