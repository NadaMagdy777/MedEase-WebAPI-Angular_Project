import { IAddress } from 'src/app/sharedClassesAndTypes/iaddress';
import { Component } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Subscription } from 'rxjs';
import { ISpecialty } from 'src/app/sharedClassesAndTypes/Doctor/ispeciality';
import { AddressService } from 'src/app/Services/address/address.service';
import { SpecialtiesService } from 'src/app/services/specialities/specialities.service';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css'],
})
export class CarouselComponent {
  selectedSpecialtyName: string = 'All';
  selectedCity: string = 'Egypt';
  selectedRegion: string = 'All';
  selectedName: string = 'All';
  specialties: ISpecialty[] = [];
  cities: string[] = [];
  regions: string[] = [];
  allSubscriptions: Subscription[] = [];

  imgSrcsArr: string[] = [
    '../../../assets/image/home/homecarousel1-eg (1).jpg',
    '../../../assets/image/home/homecarousel1-eg (2).jpg',
    '../../../assets/image/home/homecarousel1-eg (3).jpg',
    '../../../assets/image/home/homecarousel1-eg (4).jpg',
  ];

  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 500,
    navText: ['', ''],
    autoplay: true,
    responsive: {
      0: { items: 1 },
      400: { items: 1 },
      740: { items: 1 },
      940: { items: 1 },
    },
    nav: false,
  };

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
    console.log(this.selectedSpecialtyName);
    console.log(this.selectedName);
    console.log(
      this._addressService.getAddressID(this.selectedCity, this.selectedRegion)
    );
  }
}
