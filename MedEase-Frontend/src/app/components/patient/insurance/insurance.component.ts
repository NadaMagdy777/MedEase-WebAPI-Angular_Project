import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { InsuranceService } from 'src/app/services/insurance/insurance.service';
import { PatientService } from 'src/app/services/patient/patient.service';
import { Insurance } from 'src/app/sharedClassesAndTypes/patient/insurance';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';
import { IInsurance } from 'src/app/sharedClassesAndTypes/shared/iinsurance';

@Component({
  selector: 'app-insurance',
  templateUrl: './insurance.component.html',
  styleUrls: ['./insurance.component.css']
})
export class InsuranceComponent {

  id:number = parseInt(this._userAuthService.getLoggedUserId);
  errorMessage: any;

  allInsurance:IInsurance[] = [];

  insurance:Insurance = {
    insuranceID: 0,
    insuranceNumber: 0
  };

  patient:Patient = {
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
  
  InsuranceForm:FormGroup;

  constructor(
    private _patientService:PatientService,
    private  _insuranceService:InsuranceService,
    private _userAuthService:UserAuthService,
    public actRoute: ActivatedRoute,
    public router: Router,
    private fb:FormBuilder) 
    {
      this.InsuranceForm = this.fb.group({
        insuranceNumber:[,[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
        insuranceID:[]
      });

      this.InsuranceForm.get('insuranceNumber')?.valueChanges.subscribe((data) => {
        this.insurance.insuranceNumber = data;
      });
      this.InsuranceForm.get('insuranceID')?.valueChanges.subscribe((data) => {
        this.insurance.insuranceID = data;
      });

    }

  get insuranceNumber()
  {
    return this.InsuranceForm.get('insuranceNumber');
  }
  get lname()
  {
    return this.InsuranceForm.get('lname');
  }

  ngOnInit(): void {

    this._insuranceService.getInsurances()
    .subscribe({
      next:(data: any)=> {
          let dataJson = JSON.parse(JSON.stringify(data))
          this.allInsurance = dataJson.data;
          console.log(data);
          
      },
      error:(error: any)=>this.errorMessage=error,
    });   

    this._patientService
    .GetPatientById(this.id)
    .subscribe({
      next:(data: any)=> {
          let dataJson = JSON.parse(JSON.stringify(data))
          this.patient = dataJson.data;
          console.log(this.patient);
          
          if(this.patient.insurance != null){
            this.LoadFormData();
          }
      },
      error:(error: any)=>this.errorMessage=error,
    });   
  }
  LoadFormData(): void {
    this.InsuranceForm.patchValue({
      insuranceNumber:this.patient.insurance.insuranceNumber,
      insuranceID:this.patient.insurance.insuranceID,
    })
  }

  Save():void{
    
    if(this.patient.insurance == null){
      this._patientService.AddInsurance(this.id, this.insurance)
      .subscribe(response => {
        console.log(response)
      });
      alert('saved successfully');
    }

    else{
      this._patientService.UpdateInsurance(this.id, this.insurance)
      .subscribe(response => {
        console.log(response)
      });
      alert('updated successfully');
    }
  }
  Cancel():void{
    if(window.confirm('Are you sure, you want to cancel, you are about to lose the new data?')){
      
      if(this.patient.insurance != null){
        this.LoadFormData(); 
      }
      else{
        this.InsuranceForm.reset();
      }
    }
  }
}
