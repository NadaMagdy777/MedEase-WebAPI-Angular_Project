import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AddressService } from 'src/app/services/address/address.service';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { DoctorService } from 'src/app/services/Doctor/doctor.service';
import { ImageService } from 'src/app/services/image.service';
import { Doctor } from 'src/app/sharedClassesAndTypes/Doctor/Doctor';
import { DoctorEdit } from 'src/app/sharedClassesAndTypes/Doctor/doctorEdit';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent {

  id:number = parseInt(this._userAuthService.getLoggedUserId);
  errorMessage: any;

  doctor:Doctor = {
    id: 0,
    fees: 0,
    faculty: '',
    name: '',
    phoneNumber: '',
    gender: undefined,
    age: 0,
    building: 0,
    waitingTime: 0,
    rating: undefined,
    street: '',
    addressDto:undefined,
    doctorSubspiciality:[],
    specialityName: '',
    doctorcertificates: undefined,
    doctorcerInsurance: undefined,
    visitors: 0,
    email: '',
    profilePicture:undefined,
    clincRating:0,
    specialityID:0

  };

  doctorEdit:DoctorEdit = {
    fees: 0,
    firstName: '',
    lastName: '',
    phoneNumber: '',
    profilePicture:'',
    building: 0,
    region: '',
    city: '',
    street: '',
    email: ''
  };

  imageUrl:string='';
  imageBytes:Uint8Array | undefined;
  CitiesList:string[] = [];
  RegionsList:string[] = [];
  selectedCity: string = 'Egypt';
  selectedRegion: string = 'Egypt';

  EditProfileForm:FormGroup;
  imageForm:FormGroup;

  isUpdated:boolean = false;

  constructor(
    private _doctorService:DoctorService,
    private _addressService:AddressService,
    private _imageService: ImageService,
    private _userAuthService:UserAuthService,
    public actRoute: ActivatedRoute,
    public router: Router,
    private fb:FormBuilder) {

      this.imageForm = this.fb.group({
        profilePicture:[[],[Validators.required]],
      });

      this.imageForm.get('profilePicture')?.valueChanges.subscribe((data) => {
        this.doctorEdit.profilePicture = data;
      });

      this.EditProfileForm = this.fb.group({

        fname:['',[Validators.required,Validators.minLength(3),Validators.maxLength(30)]],
        lname:['',[Validators.required,Validators.minLength(3),Validators.maxLength(30)]],
        email:['',[Validators.required,Validators.email]],
        phone:['',[Validators.required]],
        fees:[0,[Validators.required]],
        waitingTime:[0,[Validators.required]],
        address:this.fb.group({
          building:[0,[Validators.required]],
          street:['',[Validators.required,Validators.minLength(2),Validators.maxLength(50)]],
          region:['',[Validators.required]],
          city:['',[Validators.required]]
        })
      });
      
      this.EditProfileForm.get('fname')?.valueChanges.subscribe((data) => {
        this.doctorEdit.firstName = data;
      });
      this.EditProfileForm.get('lname')?.valueChanges.subscribe((data) => {
        this.doctorEdit.lastName = data;
      });
      this.EditProfileForm.get('phone')?.valueChanges.subscribe((data) => {
        this.doctorEdit.phoneNumber = data;
      });
      this.EditProfileForm.get('email')?.valueChanges.subscribe((data) => {
        this.doctorEdit.email = data;
      });
      this.EditProfileForm.get('fees')?.valueChanges.subscribe((data) => {
        this.doctorEdit.fees = data;
      });
      this.EditProfileForm.controls['address'].get('building')?.valueChanges.subscribe((data) => {
        this.doctorEdit.building = data;
      });
      this.EditProfileForm.controls['address'].get('street')?.valueChanges.subscribe((data) => {
        this.doctorEdit.street = data;
      });
      this.EditProfileForm.controls['address'].get('region')?.valueChanges.subscribe((data) => {
        this.doctorEdit.region = data;
      });
      this.EditProfileForm.controls['address'].get('city')?.valueChanges.subscribe((data) => {
        this.doctorEdit.city = data;
        this.selectedCity = data;
        this.updateCity();
      });

    }
    ngOnInit(): void {

      this._doctorService
      .GetDoctorByID(this.id)
      .subscribe({
        next:(data: any)=> {
            let dataJson = JSON.parse(JSON.stringify(data))
            this.doctor = dataJson.data;
            this.LoadFormData();
            console.log(this.doctor);
            
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
      this.imageUrl = this._imageService.base64ArrayToImage(this.doctor.profilePicture);
      this.EditProfileForm.patchValue({        
        profilePicture: this.imageUrl,
      });
      this.EditProfileForm.patchValue({
        fname:this.doctor.name.substring(0, this.doctor.name.indexOf(' ')),
        lname:this.doctor.name.substring(this.doctor.name.indexOf(' ') + 1),
        email:this.doctor.email,
        phone:this.doctor.phoneNumber,
        fees:this.doctor.fees,
        address:{
          building:this.doctor.building,
          street:this.doctor.street,
          region:this.doctor.addressDto.region,
          city:this.doctor.addressDto.city,
        }
      });
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
  get fees()
  {
    return this.EditProfileForm.get('fees');
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
  get profilePicture()
  {
    return this.imageForm.get('profilePicture');
  }
  updateCity(): void {
    this._addressService.updateRegions(this.selectedCity);
    this.selectedRegion = this.doctorEdit.region;
    console.log(this.selectedRegion);
  }

  public async ProcessFile(event:any){
    const file = event.target.files[0];
    if(file){
      const reader = new FileReader();
      reader.onload = async (e:any)=>{
        this.imageUrl = e.target.result; 
        this.doctorEdit.profilePicture = await this._imageService.imageToBase64Array(this.imageUrl);
      };
      reader.readAsDataURL(file);
    }
  } 

  updateDoctorInfo():void {
    console.log(this.doctorEdit)
    console.log(this._addressService.getAddressID(this.selectedCity, this.selectedRegion))
    if(window.confirm('Are you sure, you want to update?')){
      this._doctorService.UpdateDoctorInfo(this.id, this.doctorEdit)
      .subscribe(); 
    }
    this.isUpdated = !this.isUpdated;
  }

  Cancel() : void { 
    if(window.confirm('Are you sure, you want to cancel, you are about to lose the new data?')){
      this.LoadFormData(); 
    }

  }
}
