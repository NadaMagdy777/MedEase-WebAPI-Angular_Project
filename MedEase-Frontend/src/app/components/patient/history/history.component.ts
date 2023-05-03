import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PatientService } from 'src/app/services/patient/patient.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent {
  patientId!:number
  patient!:Patient
  errorMessage: any;
  constructor(private PatientService:PatientService,private router:Router ,private route:ActivatedRoute){
    
    
  }
  ngOnInit(): void {
    this.patientId= this.route.snapshot.params['id']

    this.PatientService.GetPatientById(this.patientId).subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.patient=dataJson.data
         console.log(this.patient)
       
      
        


      },
      error:error=>this.errorMessage=error
    })

}
}
