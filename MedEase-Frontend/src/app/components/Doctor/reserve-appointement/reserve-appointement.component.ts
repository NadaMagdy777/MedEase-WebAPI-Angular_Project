import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DoctorService } from 'src/app/Services/Doctor/doctor.service';
import { IreserveAppointement } from 'src/app/SharedClassesAndTypes/Doctor/IReserveAppointement';

@Component({
  selector: 'app-reserve-appointement',
  templateUrl: './reserve-appointement.component.html',
  styleUrls: ['./reserve-appointement.component.css']
})
export class ReserveAppointementComponent  {
  date:Date = new Date();
  patientID:number = 0;
  doctorID:number = 0;
  hasInsurance: boolean = false;
  hasInvestigations:boolean = false;
  hasImage:boolean = false;

  state:any;
  insurances:[]=[];
  data!:IreserveAppointement;

  registerationForm=this.fb.group({
    insurancesId:[''],
    description:[''],
    image:[''],
  })

  changehasinsurence(){
    this.hasInsurance = !this.hasInsurance;
    this.data.hasInsurance = this.hasInsurance;
    if(this.hasInsurance)
    {        
      this.insurancesId?.setValidators(Validators.required);
    }
    else
    {
      this.insurancesId?.clearValidators();
    }
    this.insurancesId?.updateValueAndValidity();
  }

  changehasInvestigations(){
    this.hasInvestigations = !this.hasInvestigations;
    this.data.hasInvestigations = this.hasInvestigations;

    if(this.hasInvestigations)
    {        
      this.description?.setValidators(Validators.required);
    }
    else
    {
      this.description?.clearValidators();
    }
    this.description?.updateValueAndValidity();
  }

  changehasImage(){
    this.hasImage = !this.hasImage;
    this.data.hasImage = this.hasImage;
    if(this.hasImage)
    {     
      this.image?.setValidators(Validators.required);
    }
    else
    {
      this.image?.clearValidators();
    }
    this.image?.updateValueAndValidity();
  }
  addPhoto(event:any){
    // if(this.hasImage)
    //   this.data.image = <File> event.target.files[0];
  }

  get insurancesId(){
    return this.registerationForm.get('insurancesId');
  }

  get description(){
    return this.registerationForm.get('description');
  }

  get image(){
    return this.registerationForm.get('image');
  }

  constructor(private fb:FormBuilder,private router:Router,private doctorService:DoctorService) { 
    this.state = this.router.getCurrentNavigation()?.extras.state;
    this.data = {
                  date : new Date(),
                  description:"",
                  doctorID:0,
                  hasImage:this.hasImage,
                  hasInsurance:this.hasInsurance,
                  hasInvestigations:this.hasInvestigations,
                  insurancesId:0,
                  patientID:1,
                  image:""
                }
  }

  ngOnInit(): void {
    
    //extract date from router
    let selectedDate = this.state['date'].split('/');
    let selectedtimestring = this.state['time'].split(' ');
    let selectedtime = selectedtimestring[0].split(':');

    if(selectedtimestring[1] === 'PM'){
      selectedtime[0] = (+selectedtime[0] + 12).toString();
    }
    
    //set the date to the date the user select
    this.date = new Date(selectedDate[2], +selectedDate[0] - 1,selectedDate[1],+selectedtime[0],+selectedtime[1],+selectedtime[2]);
    
    this.data.date = this.date;
    
    //set doctor id to the selected one 
    this.doctorID = this.state['doctor']

    this.data.doctorID = this.doctorID;

    this.data.patientID = 1; ///////////////////////////////////////////////////////////// will change

    console.log(this.date,this.doctorID);
  
  }

  // onFileSelect(event) {
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //     this.uploadForm.get('profile').setValue(file);
  //   }
  // }

  submitData()
  {
    // const formData = new FormData();
    // formData.append()
    if(this.hasInsurance)
      this.data.insurancesId = <number> <unknown>this.insurancesId?.value;
    if(this.hasInvestigations)
      this.data.description = <string> this.description?.value;

    this.doctorService.ReserveAppointement(this.data).subscribe({
      next:(res)=>{console.log(res)},
      error:(error)=>{console.log(error)}
    });      
    console.log(this.data)
  }


}
