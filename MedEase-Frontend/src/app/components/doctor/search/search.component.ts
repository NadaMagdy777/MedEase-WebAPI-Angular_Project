import { IAddress } from 'src/app/sharedClassesAndTypes/iaddress';
import { Component } from '@angular/core';
import { ISpecialty } from 'src/app/sharedClassesAndTypes/Doctor/ispeciality';
import { Subscription } from 'rxjs';
import { SpecialtiesService } from 'src/app/services/specialities/specialities.service';
import { AddressService } from 'src/app/Services/address/address.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  selectedSpecialtyId: number = 0;
  selectedCity: string = 'Egypt';
  selectedRegion: string = '0';
  selectedName: string = 'NotSelected';
  specialties: ISpecialty[] = [];
  cities: string[] = [];
  regions: string[] = [];
  allSubscriptions: Subscription[] = [];


  constructor(
    private _addressService: AddressService,
    private _specialtiesServices: SpecialtiesService
  ) {}

  ngOnDestroy(): void {
    this.allSubscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._addressService.getCities().subscribe((response) => {
        this.cities = response;
      })
    );
    this.allSubscriptions.push(
      this._addressService.getRegions().subscribe((response) => {
        this.regions = response;
      })
    );
    this.allSubscriptions.push(
      this._specialtiesServices.getSpecialties().subscribe((response) => {
        this.specialties = response;
      })
    );
  }

  updateCity(): void {
    this._addressService.updateRegions(this.selectedCity);
  }

  search() {
    console.log(this.selectedCity);
    console.log(this.selectedRegion);
    console.log(this.selectedSpecialtyId);
    console.log(this.selectedName);
    console.log(
      this._addressService.getAddressID(this.selectedCity, this.selectedRegion)
    );
  }
}
