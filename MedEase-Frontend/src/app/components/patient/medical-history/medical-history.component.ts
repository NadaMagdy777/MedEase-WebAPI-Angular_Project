import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { PatientService } from 'src/app/services/patient/patient.service';
import { MedicalHistory } from 'src/app/sharedClassesAndTypes/patient/medicalHistory';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';

@Component({
  selector: 'app-medical-history',
  templateUrl: './medical-history.component.html',
  styleUrls: ['./medical-history.component.css']
})
export class MedicalHistoryComponent {

  id:number = parseInt(this._userAuthService.getLoggedUserId);
  errorMessage: any;
  patient:Patient={
    firstName: '',
    lastName: '',
    phoneNumber: '',
    building: 0,
    street: '',
    region: '',
    city: '',
    birthDate: undefined,
    email: '',
    insurance: undefined,
    history: undefined
  };
  hasMedicalHistory:boolean = false;
  newMedicalHistory:boolean = false;

  medicalHistory:MedicalHistory = {
    hasAllergies: false,
    hadSurgery: false,
    hasChronicIllnesses: false,
    takeMedications: false,
    hasHospitalized: false,
    isSmoking: false
  };

  MedicalHistiryForm:FormGroup;

  constructor(
    private _patientService:PatientService,
    private _userAuthService:UserAuthService,
    private fb:FormBuilder
  )
  {
    this.MedicalHistiryForm = this.fb.group({

      hasAllergies:['false',[Validators.required]],
      hadSurgery:['false',[Validators.required]],
      takeMedications:['false',[Validators.required]],
      hasHospitalized:['false',[Validators.required]],
      isSmoking:['false',[Validators.required]],
      hasChronicIllnesses:['false',[Validators.required]],     
    });

    this.MedicalHistiryForm.get('hasAllergies')?.valueChanges.subscribe((data) => {
      this.medicalHistory.hasAllergies = JSON.parse(data);
    });
    this.MedicalHistiryForm.get('hadSurgery')?.valueChanges.subscribe((data) => {
      this.medicalHistory.hadSurgery = JSON.parse(data);
    });
    this.MedicalHistiryForm.get('takeMedications')?.valueChanges.subscribe((data) => {
      this.medicalHistory.takeMedications = JSON.parse(data);
    });
    this.MedicalHistiryForm.get('hasHospitalized')?.valueChanges.subscribe((data) => {
      this.medicalHistory.hasHospitalized = JSON.parse(data);
    });
    this.MedicalHistiryForm.get('isSmoking')?.valueChanges.subscribe((data) => {
      this.medicalHistory.isSmoking = JSON.parse(data);
    });
    this.MedicalHistiryForm.get('hasChronicIllnesses')?.valueChanges.subscribe((data) => {
      this.medicalHistory.hasChronicIllnesses = JSON.parse(data);
    });

  }

  get hasAllergies()
  {
    return this.MedicalHistiryForm.get('hasAllergies');
  }
  get hadSurgery()
  {
    return this.MedicalHistiryForm.get('hadSurgery');
  }
  get takeMedications()
  {
    return this.MedicalHistiryForm.get('takeMedications');
  }
  get hasHospitalized()
  {
    return this.MedicalHistiryForm.get('hasHospitalized');
  }
  get hasChronicIllnesses()
  {
    return this.MedicalHistiryForm.get('hasChronicIllnesses');
  }
  get isSmoking()
  {
    return this.MedicalHistiryForm.get('isSmoking');
  }

  ngOnInit(): void 
  {
    this._patientService
    .GetPatientById(this.id)
    .subscribe({
      next:(data: any)=> {
          let dataJson = JSON.parse(JSON.stringify(data))
          this.patient = dataJson.data;

          if(this.patient?.history != null){
            this.hasMedicalHistory = true;
            this.LoadFormData();
          }
          else{
            this.MedicalHistiryForm.reset();
          }
      },
      error:(error: any)=>this.errorMessage=error,
    });   
  }

  LoadFormData(): void {

    this.MedicalHistiryForm.patchValue({

      hasAllergies:this.patient.history.hasAllergies ? 'true':'false',
      hadSurgery:this.patient.history.hadSurgery ? 'true':'false',
      takeMedications:this.patient.history.takeMedications ? 'true':'false',
      hasHospitalized:this.patient.history.hasHospitalized ? 'true':'false',
      hasChronicIllnesses:this.patient.history.hasChronicIllnesses ? 'true':'false',
      isSmoking:this.patient.history.isSmoking ? 'true':'false',
    });
    
  }

  Save() : void {
    this._patientService.AddMedicalHistory(this.id, this.medicalHistory)
    .subscribe(response => {
      console.log(response)
    });
    
    alert('saved successfully');
  }
  Update() : void {
    if(window.confirm('Are you sure, you want to update?')){
      this._patientService.UpdateMedicalHistory(this.id, this.medicalHistory)
      .subscribe(response => {
        console.log(response)
      });
    }
  }
  Cancel() : void { 
    if(window.confirm('Are you sure, you want to cancel, you are about to lose the new data?')){
      
      if(this.hasMedicalHistory){
        this.LoadFormData(); 
      }
      else{
        this.MedicalHistiryForm.reset();
      }
    }
  }
}
