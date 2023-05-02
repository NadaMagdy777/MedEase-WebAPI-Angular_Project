import { ISpecialty } from 'src/app/SharedClassesAndTypes/Doctor/ispeciality';
import { environment } from './../../../environments/environment';
import { IApiResponse } from '../../sharedClassesAndTypes/iapi-response';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { ISubSpecialty } from 'src/app/SharedClassesAndTypes/Doctor/isub-specialty';

@Injectable({
  providedIn: 'root',
})
export class SpecialtiesService implements OnDestroy {
  private specialties: BehaviorSubject<ISpecialty[]> = new BehaviorSubject<
    ISpecialty[]
  >([]);
  private allSubSpecialties: ISubSpecialty[] = [];
  subSpecialties: BehaviorSubject<ISubSpecialty[]> = new BehaviorSubject<
    ISubSpecialty[]
  >([]);
  private allSubscriptions: Subscription[] = [];

  constructor(private _httpClient: HttpClient) {    
    this.allSubscriptions.push(
      this.fetchSpecialties().subscribe((response) => {
        if (response.success) {
          this.specialties.next(response.data);
        }
      })
    );

    this.allSubscriptions.push(
      this.fetchSubSpecialties().subscribe((response) => {
        if (response.success) {
          this.allSubSpecialties = response.data;
        }
      })
    );
  }

  ngOnDestroy(): void {
    this.allSubscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  private fetchSpecialties(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(
      environment.apiUrl + '/General/Speciality'
    );
  }

  private fetchSubSpecialties(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(
      environment.apiUrl + '/General/SubSpeciality'
    );
  }

  getSpecialties(): Observable<ISpecialty[]> {
    return this.specialties.asObservable();
  }

  getSubSpecialties(): Observable<ISubSpecialty[]> {
    return this.subSpecialties.asObservable();
  }

  updateSubSpecialties(specialtyID: number): void {
    console.log("spc upd srv");

    this.subSpecialties.next(
      this.allSubSpecialties.filter(
        (subSpecialty) => subSpecialty.sepcID === specialtyID
      )
    );
  }
}
