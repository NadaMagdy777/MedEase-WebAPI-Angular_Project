import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DoctorService } from 'src/app/services/Doctor/doctor.service';
import { IreserveAppointement } from 'src/app/sharedClassesAndTypes/Doctor/IReserveAppointement';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';

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
  selectedtimestring:string="";
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
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      const base64String = reader.result as string;
      const base64Data = base64String.split(",")[1];
      this.data.image = base64Data;
      // this.userRegisterForm.value.licenseImgTemp = base64String;
    };
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

  constructor(private fb:FormBuilder,private router:Router,private doctorService:DoctorService,private userAuthService: UserAuthService) { 
    this.state = this.router.getCurrentNavigation()?.extras.state;
    this.data = {
                  date : "",
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
    this.selectedtimestring = this.state['time'];
    let selectedtimestringArr = this.state['time'].split(' ');
    let selectedtime = selectedtimestringArr[0].split(':');

    if(selectedtimestringArr[1] === 'PM'){
      if(selectedtime[0] === '12'){
        selectedtime[0] = '12'
      }
      else{
        selectedtime[0] = (+selectedtime[0] + 12).toString();
      }
    }else{
      if(selectedtime[0] === '12')
        selectedtime[0] = '00'
    }
    
    //set the date to the date the user select
    this.date = new Date(selectedDate[2], +selectedDate[0] - 1,selectedDate[1],+selectedtime[0],+selectedtime[1],+selectedtime[2]);
    
    this.data.date = this.date.toLocaleString();
    
    //set doctor id to the selected one 
    this.doctorID = this.state['doctor']

    this.data.doctorID = this.doctorID;
    
    this.data.patientID = <number> <unknown>this.userAuthService.getLoggedUserId;    
  
  }

  submitData()
  {
    if(this.hasInsurance)
      this.data.insurancesId = <number> <unknown>this.insurancesId?.value;
    if(this.hasInvestigations)
      this.data.description = <string> this.description?.value;
      
    this.doctorService.ReserveAppointement(this.data).subscribe({
      next:(res)=>{console.log(res)},
      error:(error)=>{console.log(error)}
    });
    this.router.navigate(["Home"]);//////////////////////////////////////////////////////////////////////

     
  }


}
