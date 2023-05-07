import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from 'src/app/services/Doctor/doctor.service';
import { ImageService } from 'src/app/services/image.service';
import { SpecialtiesService } from 'src/app/services/specialities/specialities.service';
import { Doctor } from 'src/app/sharedClassesAndTypes/Doctor/Doctor';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';

@Component({
  selector: 'app-home-banner',
  templateUrl: './home-banner.component.html',
  styleUrls: ['./home-banner.component.css']
})
export class HomeBannerComponent {
  DoctorList!:Doctor[]
  specialties$ = this._specialtiesService.getSpecialties();
  constructor(private doctorService:DoctorService, private router:Router, private imageService:ImageService,    private _specialtiesService: SpecialtiesService,
    private specialtiesService: SpecialtiesService){}
  ngOnInit(): void {
    this.doctorService.GetTopRated().subscribe({
      next:(response)=>{
        this.DoctorList = <Doctor[]>response.data;
        console.log(this.DoctorList);
        this.DoctorList.forEach(d=>{
          d.profilePicture = this.imageService.base64ArrayToImage(d.profilePicture);
        })
      },
      error:(error)=>{console.log(error);}
    })
  }
  gotToDetails(id:number){
    this.router.navigate(['/doctor/details/'+id])
  }

}
