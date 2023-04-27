import { Component, NgZone } from '@angular/core';
import { Patient } from 'src/app/SharedClassesAndTypes/patient/patient';
import { PatientService } from 'src/app/services/patient/patient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  id:number = 2;//this.actRoute.snapshot.params['id'];
  errorMessage: any;

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
    insuranceName: '',
    history: undefined
  };

  EditProfileForm:FormGroup;

  constructor(
    private _patientService:PatientService,
    public actRoute: ActivatedRoute,
    public router: Router,
    private fb:FormBuilder) 
    {
      this.EditProfileForm = this.fb.group({

        fname:['',[Validators.required,Validators.minLength(3),Validators.maxLength(30)]],
        lname:['',[Validators.required,Validators.minLength(3),Validators.maxLength(30)]],
        email:['',[Validators.required,Validators.email]],
        phone:['',[Validators.required]],
        address:this.fb.group({
          building:[0,[Validators.required]],
          street:['',[Validators.required]],
          region:['',[Validators.required]],
          city:['',[Validators.required]]
        })
      });

      this.EditProfileForm.get('fname')?.valueChanges.subscribe((data) => {
        this.patient.firstName = data;
      });
      this.EditProfileForm.get('lname')?.valueChanges.subscribe((data) => {
        this.patient.lastName = data;
      });
      this.EditProfileForm.get('phone')?.valueChanges.subscribe((data) => {
        this.patient.phoneNumber = data;
      });
      this.EditProfileForm.get('email')?.valueChanges.subscribe((data) => {
        this.patient.phoneNumber = data;
      });
      this.EditProfileForm.get('building')?.valueChanges.subscribe((data) => {
        this.patient.phoneNumber = data;
      });
      this.EditProfileForm.get('street')?.valueChanges.subscribe((data) => {
        this.patient.phoneNumber = data;
      });
      this.EditProfileForm.get('region')?.valueChanges.subscribe((data) => {
        this.patient.phoneNumber = data;
      });
      this.EditProfileForm.get('city')?.valueChanges.subscribe((data) => {
        this.patient.phoneNumber = data;
      });
    }
  
  ngOnInit(): void {
    this._patientService
    .GetPatientById(this.id)
    .subscribe({
      next:(data: any)=> {
          let dataJson = JSON.parse(JSON.stringify(data))
          this.patient = dataJson.data;
          this.LoadFormData();
      },
      error:(error: any)=>this.errorMessage=error,
    });   

  }

  LoadFormData(): void {
    this.EditProfileForm.patchValue({
      fname:this.patient.firstName,
      lname:this.patient.lastName,
      email:this.patient.email,
      phone:this.patient.phoneNumber,
      address:{
        building:this.patient.building,
        street:this.patient.street,
        region:this.patient.region,
        city:this.patient.city,
      }
    })
  }

  get fname()
  {
    return this.EditProfileForm.get('fname');
  }
  get lname()
  {
    return this.EditProfileForm.get('lname');
  }
  get phone()
  {
    return this.EditProfileForm.get('phone');
  }
  get email()
  {
    return this.EditProfileForm.get('email');
  }
  get building()
  {
    return this.EditProfileForm.controls['address'].get('building');
  }
  get street()
  {
    return this.EditProfileForm.controls['address'].get('street');
  }
  get region()
  {
    return this.EditProfileForm.controls['address'].get('region');
  }
  get city()
  {
    return this.EditProfileForm.controls['address'].get('city');
  }

  updatePatientInfo():void {
    console.log(this.patient)

    if(window.confirm('Are you sure, you want to update?')){
      this._patientService.UpdatePatientInfo(this.id, this.patient)
      .subscribe(data => this.router.navigate(['/accountProfile']));
    }
  }

  Cancel() : void { 
    if(window.confirm('Are you sure, you want to cancel, you are about to lose the new data?')){
      this.LoadFormData(); 
    }
  }
}
