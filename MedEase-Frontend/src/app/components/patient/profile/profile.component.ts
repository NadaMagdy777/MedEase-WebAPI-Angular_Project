import { Component } from '@angular/core';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';
import { PatientService } from 'src/app/services/patient/patient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AddressService } from 'src/app/services/address/address.service';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  id:number = parseInt(this._userAuthService.getLoggedUserId);
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
    insurance: undefined,
    history: undefined
  };

  CitiesList:string[] = [];
  RegionsList:string[] = [];
  selectedCity: string = 'Egypt';
  selectedRegion: string = 'Egypt';

  EditProfileForm:FormGroup;
  
  constructor(
    private _patientService:PatientService,
    private _addressService:AddressService,
    private _userAuthService:UserAuthService,
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
          street:['',[Validators.required,Validators.minLength(2),Validators.maxLength(50)]],
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
        this.patient.email = data;
      });
      this.EditProfileForm.controls['address'].get('building')?.valueChanges.subscribe((data) => {
        this.patient.building = data;
      });
      this.EditProfileForm.controls['address'].get('street')?.valueChanges.subscribe((data) => {
        this.patient.street = data;
      });
      this.EditProfileForm.controls['address'].get('region')?.valueChanges.subscribe((data) => {
        this.patient.region = data;
      });
      this.EditProfileForm.controls['address'].get('city')?.valueChanges.subscribe((data) => {
        this.patient.city = data;
        this.selectedCity = data;
        this.updateCity();
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

    this._addressService.getCities().subscribe({
      next:(data:any)=>{
        this.CitiesList = data;
      },
      error:(error: any)=>this.errorMessage=error,
    });

    this._addressService.getRegions().subscribe({
      next:(data:any)=>{
        this.RegionsList = data;
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

  updateCity(): void {
    this._addressService.updateRegions(this.selectedCity);
    this.selectedRegion = this.patient.region;
    console.log(this.selectedRegion);
  }

  updatePatientInfo():void {
    if(window.confirm('Are you sure, you want to update?')){
      this._patientService.UpdatePatientInfo(this.id, this.patient)
      .subscribe(); 
    }
  }

  Cancel() : void { 
    if(window.confirm('Are you sure, you want to cancel, you are about to lose the new data?')){
      this.LoadFormData(); 
    }
  }
}
