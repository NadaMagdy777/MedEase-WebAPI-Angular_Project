import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { Subscription } from 'rxjs';
import { AddressService } from 'src/app/services/address/address.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Gender } from 'src/app/sharedClassesAndTypes/enums/gender';

@Component({
  selector: 'app-patient-register',
  templateUrl: './patient-register.component.html',
  styleUrls: ['./patient-register.component.css'],
})
export class PatientRegisterComponent {
  constructor(
    private _addressService: AddressService,
    private _userAuthService: UserAuthService
  ) {}

  ngOnInit() {
    // this.allSubscriptions.push(
    this.userRegisterForm
      .get('city')
      ?.valueChanges.subscribe((selectedCity) => {
        this._addressService.updateRegions(selectedCity);
      });
    // );

    // this.allSubscriptions.push(
    //   this._addressService.getCities().subscribe(cities => {
    //     this.cities = cities;
    //     })
    //   );
  }

  onCitySelect(citySelect: HTMLSelectElement): void {
    this._addressService.updateRegions(citySelect.value);
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

  onRegisterFormSubmit() {
    console.log(this.userRegisterForm.value);
    this.isLoading = true;
    this.allSubscriptions.push(
      this._userAuthService
        .registerPatient(this.userRegisterForm.value)
        .subscribe((response) => {
          if (response.success) {
            console.log(response);
            this.isLoading = false;
            this._userAuthService.confirmUserLogin(response.data.token);
          } else {
            this.serverErrorMsg = response.message;
            console.log(response);
            this.isLoading = false;
          }
        })
    );
  }

  allSubscriptions: Subscription[] = [];
  Gender = Gender;
  cities$ = this._addressService.getCities();
  regions$ = this._addressService.getRegions();
  isLoading: boolean = false;
  serverErrorMsg: any = '';

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
  });
}
