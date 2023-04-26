import { SpecialtiesService as SpecialtiesService } from './../../../services/specialities/specialities.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ISpeciality } from 'src/app/SharedClassesAndTypes/Doctor/ispeciality';
import { AddressService } from 'src/app/services/address/address.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit, OnDestroy {
  allSubscriptions: Subscription[] = [];
  cities: string[] = [];
  regions: string[] = [];
  specialties: ISpeciality[] = [];

  selectedCity: string = 'Cairo';
  selectedRegion: string = '';
  selectedSpecialty: number = 0;
  selectedDoctor: string = '';

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
        this.specialties = response.data;
      })
    );
  }

  updateCity(): void {
    this._addressService.updateRegions(this.selectedCity);
  }

  search() {
    console.log(this.selectedCity);
    console.log(this.selectedRegion);
    console.log(this.selectedSpecialty);
    console.log(this.selectedDoctor);

    console.log(this._addressService.getAddressID(this.selectedCity, this.selectedRegion));
    
  }
}
