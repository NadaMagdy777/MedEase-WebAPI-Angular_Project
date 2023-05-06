import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorService } from 'src/app/Services/Doctor/doctor.service';
import { IDiagnosisDto } from 'src/app/sharedClassesAndTypes/diagnosis/i-diagnosis-dto';

@Component({
  selector: 'app-diagnosis',
  templateUrl: './diagnosis.component.html',
  styleUrls: ['./diagnosis.component.css']
})
export class DiagnosisComponent {
  diagnosis:IDiagnosisDto={examinationID:0,details:""}
  NewDiagnosisForm:FormGroup;
  EaminationId!:number;

  constructor(private DoctorService:DoctorService,private router:Router ,private route:ActivatedRoute,
    
    private fb:FormBuilder) 
    {
      this.NewDiagnosisForm = this.fb.group({

        Description:['',[Validators.required,Validators.minLength(5),Validators.maxLength(500)]],
      })
      this.NewDiagnosisForm.get('Description')?.valueChanges.subscribe((data) => {
        this.diagnosis.details= data;
      });
    }
    get Description()
    {
      return this.NewDiagnosisForm.get('Description');
    }
    ngOnInit(): void {
      this.EaminationId= this.route.snapshot.params['id']
      console.log(this.EaminationId)
      this.diagnosis.examinationID= this.EaminationId
      console.log("hello")
      console.log(this.diagnosis.examinationID)
    }
    SaveDiagnosis(){
      
      if(window.confirm('Are you sure, you want to add this diagnosis')){
        this.DoctorService.postDiagnosis(this.diagnosis).subscribe((res) => {
          if (res.success) {
             console.log("success");
             this.router.navigate(['/doctor/Appointment/Confirmed'])
          } else {
            console.log(res.message); 
          }
        })
       
      }

    }


}
