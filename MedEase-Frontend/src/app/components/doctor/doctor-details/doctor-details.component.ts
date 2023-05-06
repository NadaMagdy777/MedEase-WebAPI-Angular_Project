import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DoctorService } from 'src/app/services/doctor/doctor.service';
import { Doctor } from 'src/app/sharedClassesAndTypes/doctor/Doctor';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Review } from 'src/app/sharedClassesAndTypes/review/review';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-doctor-details',
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.css']
})
export class DoctorDetailsComponent {

  doctorId!:number
  doctor:Doctor=new Doctor(0,0,'','','','',0,0,0,0,'',0,[],'','','',0,0,'','',0);
  doctorReview:Review[]=[] 
  SomeReview:Review[]=[]
  errorMessage: any;
  reviewShowed:number=1
  viewMoreBtnShow:boolean=true
  constructor(private DoctorService:DoctorService,private router:Router ,private route:ActivatedRoute,private _imageService:ImageService){
   
    
    
  }
  ngOnInit(): void {
    this.doctorId= this.route.snapshot.params['id']
        
    console.log(this.doctorId)
    this.DoctorService.GetDoctorByID(this.doctorId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.doctor=dataJson.data
        this.doctor.profilePicture=this._imageService.base64ArrayToImage(this.doctor.profilePicture)
        this.DoctorService.GetDoctorReviews(this.doctorId).subscribe({
          next:data=>{
            let dataJson = JSON.parse(JSON.stringify(data))
            this.doctorReview=dataJson.data
            this.SomeReview=this.doctorReview.slice(0,this.reviewShowed)
            
            
          },
          error:error=>this.errorMessage=error
        })
        
      

      },
      error:error=>this.errorMessage=error
    })

    
    
   
    
    
  }
  showReview(){
    this.reviewShowed+=1

    this.SomeReview=this.doctorReview.slice(0,this.reviewShowed)
    if(this.reviewShowed==this.doctorReview.length){
      this.viewMoreBtnShow=false
    }
  }


}
