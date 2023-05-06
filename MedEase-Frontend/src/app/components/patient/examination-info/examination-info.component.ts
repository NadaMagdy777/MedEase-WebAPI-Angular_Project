import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PatientService } from 'src/app/services/patient/patient.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';
import { AppointmentService } from 'src/app/services/appointment/appointment.service';
import { IDoctorPendingAppointmentDetailsDto } from 'src/app/sharedClassesAndTypes/appointment/i-doctor-pending-appointment-details-dto';
import { Gender } from 'src/app/SharedClassesAndTypes/enums/gender';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-examination-info',
  templateUrl: './examination-info.component.html',
  styleUrls: ['./examination-info.component.css']
})
export class ExaminationInfoComponent {
  appointmentId!:number
  patient!:Patient
  Somediagnosis:any=[]
  errorMessage: any;
  DiagnosisShowed:number=1
  viewMoreBtn:boolean=true
  
  Appointmentinfo: IDoctorPendingAppointmentDetailsDto
  ={appointmentID:0,date:new Date(),previousDiagnoses:[],patientBirthDate:new Date(),patientID
  :0,patientGender:0,patientPhone:"",
  investigation:{appointmentId:0,hasImage:false,description:"",image:""},
 status:0,patientName:"",history:{takeMedications:false,hadSurgery:false,hasAllergies:false,hasChronicIllnesses:false,hasHospitalized:false,isSmoking:false}};
Gender: any;
  constructor(private PatientService:PatientService,private router:Router ,private route:ActivatedRoute,private _appointmentService: AppointmentService,
    private _imageService:ImageService){
    
    
  }
  ngOnInit(): void {
    this.appointmentId= this.route.snapshot.params['id']
    this._appointmentService.getDoctorPendingAppointments().subscribe({
      next:data=>{
        let dataJson = JSON.parse(JSON.stringify(data))
        let appointments=dataJson.data
        console.log(appointments)
        this.Appointmentinfo=  appointments.find( (appointment:IDoctorPendingAppointmentDetailsDto) =>{
          return appointment.appointmentID==this.appointmentId
        });
        this.Appointmentinfo.investigation.image=this._imageService.base64ArrayToImage(this.Appointmentinfo.investigation.image)
        
        this.Somediagnosis=this.Appointmentinfo.previousDiagnoses.slice(0,this.DiagnosisShowed)
        console.log(this.Appointmentinfo)
        this.PatientService.GetPatientById(this.Appointmentinfo.patientID).subscribe({
          next:data=>{
            let dataJson = JSON.parse(JSON.stringify(data))
            this.patient=dataJson.data
             console.log(this.patient)
           
          
            
    
    
          },
          error:error=>this.errorMessage=error
        })
    
        
       
      
        


      },
    })

    
}
showMoreDiagnosis(){
  this.DiagnosisShowed+=1

  this.Somediagnosis=this.Appointmentinfo.previousDiagnoses.slice(0,this.DiagnosisShowed)
  if(this.DiagnosisShowed>=this.Appointmentinfo.previousDiagnoses.length){
    this.viewMoreBtn=false
  }
}

}
