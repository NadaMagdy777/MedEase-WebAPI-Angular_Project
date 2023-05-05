import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Doctor } from 'src/app/SharedClassesAndTypes/Doctor/Doctor';


@Component({
  selector: 'app-doctor-index',
  templateUrl: './doctor-index.component.html',
  styleUrls: ['./doctor-index.component.css']
})
export class DoctorIndexComponent implements OnInit ,OnChanges{
  @Input() FilterdDoctor!:Doctor[]
  DoctorList!:Doctor[]
  load:boolean=false;
  errorMessage: any;
  p:number=1;
  constructor(){
   
    
    
  }
  
  ngOnInit(): void {
    this.DoctorList=this.FilterdDoctor

  }
  
  ngOnChanges(changes: SimpleChanges): void {
      
      this.DoctorList=this.FilterdDoctor
      this.p=1



      
      
  }
 

    



  

}
