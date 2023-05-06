import { ImageService } from './../../../services/image.service';
import { Router } from '@angular/router';
import { IInsurance } from './../../../sharedClassesAndTypes/shared/iinsurance';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AddressService } from 'src/app/services/address/address.service';
import { SpecialtiesService } from 'src/app/services/specialities/specialities.service';
import { Gender } from 'src/app/sharedClassesAndTypes/enums/gender';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { InsuranceService } from 'src/app/services/insurance/insurance.service';

@Component({
  selector: 'app-doctor-register',
  templateUrl: './doctor-register.component.html',
  styleUrls: ['./doctor-register.component.css'],
})
export class DoctorRegisterComponent {
  constructor(
    private _addressService: AddressService,
    private _userAuthService: UserAuthService,
    private _specialitiesService: SpecialtiesService,
    private _insuranceService: InsuranceService,
    private _router: Router,
    private _imageService:ImageService
  ) {}

  ngOnInit() {
    this.userRegisterForm
      .get('city')
      ?.valueChanges.subscribe((selectedCity) => {
        this._addressService.updateRegions(selectedCity);
      });

    this.userRegisterForm
      .get('specialityID')
      ?.valueChanges.subscribe((selectedSpecialityID) => {
        this._specialitiesService.updateSubSpecialties(selectedSpecialityID);
      });

    this.allSubscriptions.push(
      this._insuranceService.getInsurances().subscribe((response) => {
        if (response.success) {
          this.insurances = response.data;
        }
      })
    );
  }

  onCitySelect(citySelect: HTMLSelectElement): void {
    this._addressService.updateRegions(citySelect.value);
  }

  onSpecialtySelect(specialtySelect: HTMLSelectElement): void {
    this._specialitiesService.updateSubSpecialties(
      Number.parseInt(specialtySelect.value)
    );
  }

  onRegionSelect(
    citySelect: HTMLSelectElement,
    regionSelect: HTMLSelectElement
  ): void {
    const addressID = this._addressService.getAddressID(
      citySelect.value,
      regionSelect.value
    );
    this.userRegisterForm.patchValue({ addressID });
    console.log(this.userRegisterForm.get('addressID')?.value);
  }

  onLicenseImgChange(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      const base64String = reader.result as string;
      const base64Data = base64String.split(",")[1];
      this.licenseImg = base64Data;
      // this.userRegisterForm.value.licenseImgTemp = base64String;
    };
  }

  onProfilePictureChange(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      const base64String = reader.result as string;
      const base64Data = base64String.split(",")[1];
      this.pP = base64Data;
      // console.log(this.userRegisterForm.value.profilePicture);
      console.log(base64Data);
    };
    // console.log(this.userRegisterForm.value.profilePicture);
  }

  onRegisterFormSubmit() {
    this.userRegisterForm.value.licenseImgTemp = this.licenseImg;
    this.userRegisterForm.value.profilePictureTemp = this.pP;
    this.isLoading = true;
    console.log(this.userRegisterForm.value);
    this._userAuthService.registerDoctor(this.userRegisterForm.value).subscribe(
      (response) => {
        if (response.success) {
          console.log(response);
          this.isLoading = false;
          this._userAuthService.confirmUserLogin(response.data.token)
        } else {
          this.serverErrorMsg = response.message;
          console.log(response);
          this.isLoading = false;
        }
      },
    );
  }

  allSubscriptions: Subscription[] = [];
  Gender = Gender;
  cities$ = this._addressService.getCities();
  regions$ = this._addressService.getRegions();
  specialties$ = this._specialitiesService.getSpecialties();
  subspecialties$ = this._specialitiesService.getSubSpecialties();
  insurances: IInsurance[] = [];
  isLoading: boolean = false;
  serverErrorMsg: any = '';
  licenseImg: any;
  pP: any;

  userRegisterForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    firstName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(30),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(30),
    ]),
    ssn: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]{14}$'),
    ]),
    phoneNumber: new FormControl('', [
      Validators.required,
      Validators.pattern('^01[0-2,5]{1}[0-9]{8}$'),
    ]),
    gender: new FormControl('', Validators.required),
    birthDate: new FormControl('', Validators.required),
    building: new FormControl('', Validators.required),
    street: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50),
    ]),
    city: new FormControl('', Validators.required),
    region: new FormControl('', Validators.required),
    addressID: new FormControl('', Validators.required),
    allowVisa: new FormControl(false, Validators.required),
    fees: new FormControl(0, [
      Validators.required,
      Validators.min(0),
      Validators.max(10000),
    ]),
    faculty: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(200),
    ]),
    specialityID: new FormControl('', Validators.required),
    subSpecialities: new FormControl(null),
    insurances: new FormControl(null),
    licenseImg: new FormControl(null, Validators.required),
    profilePicture: new FormControl(null, Validators.required),
  });
}
