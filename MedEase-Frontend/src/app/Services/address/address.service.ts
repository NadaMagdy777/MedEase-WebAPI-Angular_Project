import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { IResponse } from 'src/app/SharedClassesAndTypes/response';

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  // implements OnDestroy
  // allAddresses: IAddress[] = [];
  // allCities: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);
  // allCityRegions: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);
  // allSubscriptions: Subscription[] = [];

  // constructor(private _httpClient: HttpClient) {
  //   this.allSubscriptions.push(
  //     this.getAddresses().subscribe((response) => {
  //       this.allAddresses = response.data;
  //       this.allCities.next(
  //         Array.from(new Set(this.allAddresses.map((x) => x.city)))
  //       );
  //       this.allCityRegions.next(
  //         this.allAddresses
  //           .filter((address) => address.city === 'Cairo')
  //           .map((address) => address.region)
  //       );
  //     })
  //   );
  // }

  // ngOnDestroy(): void {
  //   this.allSubscriptions.forEach((subscription) => subscription.unsubscribe());
  // }

  // getAddresses(): Observable<IApiResponse> {
  //   return this._httpClient.get<IApiResponse>(
  //     'http://localhost:45829/api/Account/Addresses'
  //   );
  // }

  // getCities(): Observable<string[]> {
  //   return this.allCities.asObservable();
  // }

  // getRegions(): Observable<string[]> {
  //   return this.allCityRegions.asObservable();
  // }

  // updateRegions(city: string): void {
  //   return this.allCityRegions.next(
  //     this.allAddresses
  //       .filter((address) => address.city === city)
  //       .map((address) => address.region)
  //   );
  
  Url="http://localhost:45829/api/Account/Addresses"

  constructor(private http:HttpClient) { }

  getCities():Observable<IResponse>{    
    return this.http.get<IResponse>(this.Url);
  }

  // GetPostById(postId:number){    
  //   return this.http.get<Ipost>(`https://jsonplaceholder.typicode.com/posts/${postId}/comments`);
  // }

  // getAddressID(city: string, region: string): number | undefined {
  //   return this.allAddresses.find(
  //     (address) => address.city === city && address.region === region
  //   )?.id;
  // }
}
