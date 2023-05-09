import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Review } from 'src/app/sharedClassesAndTypes/review/review';
import { DoctorService } from 'src/app/services/doctor/doctor.service';


@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent {
  review:Review={examinationID:0,doctorRate:0,clinicRate:0,comment:"",waitingTimeinMins:0,patientName:"",appointmentID:0};
  NewReviewForm:FormGroup;
  appointmentId!:number;
 
  


  constructor(private DoctorService:DoctorService,private router:Router ,private route:ActivatedRoute,
    
    private fb:FormBuilder) 
    {
      this.NewReviewForm = this.fb.group({

        doctorRating:['',[Validators.required,Validators.min(1),Validators.max(5)]],
        clincRating:['',[Validators.required,Validators.min(1),Validators.max(5)]],
        WaitingTime:['',[Validators.required,Validators.min(1),Validators.max(300)]],
        comment:['',[Validators.required,Validators.minLength(4),Validators.maxLength(200)]],

      })
      this.NewReviewForm.get('doctorRating')?.valueChanges.subscribe((data) => {
        this.review.doctorRate= data;
      });
      this.NewReviewForm.get('clincRating')?.valueChanges.subscribe((data) => {
        this.review.clinicRate= data;
      });
      this.NewReviewForm.get('WaitingTime')?.valueChanges.subscribe((data) => {
        this.review.waitingTimeinMins= data;
      });
      this.NewReviewForm.get('comment')?.valueChanges.subscribe((data) => {
        this.review.comment= data;
      });
    }
    get doctorRating()
    {
      return this.NewReviewForm.get('doctorRating');
    }
    get clincRating()
    {
      return this.NewReviewForm.get('clincRating');
    }
    get WaitingTime()
    {
      return this.NewReviewForm.get('WaitingTime');
    }
    get comment()
    {
      return this.NewReviewForm.get('comment');
    }
    ngOnInit(): void {
      this.appointmentId= this.route.snapshot.params['id']
      console.log(this.appointmentId)
      this.review.appointmentID= this.appointmentId
      console.log("hello")
      console.log(this.review.examinationID)
    }
    SaveReview(){
        this.DoctorService.postReview(this.review).subscribe((res) => {
          if (res.success) {
             console.log("success");
             this.router.navigate(['/account/Appointment/Confirmed'])
          } else {
            console.log(res.message); 
          }
        })
       
    }
    cancel(){
      this.router.navigate(['/account/Appointment/Pending'])


    }
  

}
