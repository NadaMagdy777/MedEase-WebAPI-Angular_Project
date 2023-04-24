import { Component } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css'],
})
export class CarouselComponent {
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
      0: {
        items: 1,
      },
      400: {
        items: 1,
      },
      740: {
        items: 1,
      },
      940: {
        items: 1,
      },
    },
    nav: false,
  };
}
