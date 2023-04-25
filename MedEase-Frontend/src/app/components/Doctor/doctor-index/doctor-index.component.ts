import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DoctorService } from 'src/app/Services/Doctor/doctor.service';
import { Doctor } from 'src/app/SharedClassesAndTypes/Doctor/Doctor';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';


@Component({
  selector: 'app-doctor-index',
  templateUrl: './doctor-index.component.html',
  styleUrls: ['./doctor-index.component.css']
})
export class DoctorIndexComponent implements OnInit{
  DoctorList!:Doctor[]
  errorMessage: any;
  p:number=1;
  constructor(private DoctorService:DoctorService,private router:Router){
   
    
    
  }
  ngOnInit(): void {
    this.DoctorService.GetAllDoctors().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        this.DoctorList=dataJson.data
       console.log(this.DoctorList)},
      error:error=>this.errorMessage=error
    })
    



  }

}
