import { Component } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { ISpeciality } from 'src/app/SharedClassesAndTypes/Doctor/ispeciality';
import { AddressService } from 'src/app/Services/address/address.service';
import { SpecialtiesService } from 'src/app/Services/specialities/specialities.service';
import { Subscription } from 'rxjs';
import { IAddress } from 'src/app/SharedClassesAndTypes/iaddress';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css'],
})
export class CarouselComponent {
  selectedSpecialtyId:number=0;
  selectedCityId:number=0;
  selectedRegionId:number=0;
  Name:string="";
  Specialties:ISpeciality[]=[];
  Cities:string[]=[];
  Regons:string[]=[];
  allSubscriptions: Subscription[] = [];

  constructor(private addressService: AddressService, private specialtiesServices: SpecialtiesService) 
  {

  }
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
    responsive: {0: {items: 1},400: {items: 1},740: {items: 1},940: {items: 1}},
    nav: false,
  };
  ngOnDestroy(): void {
    this.allSubscriptions.forEach((subscription) => subscription.unsubscribe());
  }
  ngOnInit(): void {
    this.allSubscriptions.push(
      this.addressService.getCities().subscribe((response) => {
        
        console.log(response.data[0]);
        console.log(response.data[0].City);
        this.Cities = response.data.map(a => a.City);
        
        // console.log(this.Cities);
        
      })
    );
    // this.allSubscriptions.push(
    //   this.addressService.getRegions().subscribe((response) => {
    //     this.Regons = response;
    //   })
    // );
    // this.allSubscriptions.push(
    //   this.specialtiesServices.getSpecialties().subscribe((response) => {
    //     this.Specialties = response.data;
    //   })
    // );
  }

  updateCity(): void {
  //   this.addressService.updateRegions(this.Cities[this.selectedCityId]);
  //   console.log()
  }

  search() {
    console.log(this.selectedCityId);
    console.log(this.selectedRegionId);
    console.log(this.selectedSpecialtyId);
    console.log(this.Name);

    // console.log(this.addressService.getAddressID(this.Cities[this.selectedCityId], this.Regons[this.selectedRegionId]));
    
  }



}
