import { IApiResponse } from './../../sharedClassesAndTypes/iapi-response';
import { environment } from './../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private _httpClient: HttpClient) {}

  baseUrl: string = environment.apiUrl + '/';
  baseDocUrl: string = environment.apiUrl + '/Appointment/Doctor/';
  basePtUrl: string = environment.apiUrl + '/Appointment/Patient/';

  getDoctorPendingAppointments(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(this.baseDocUrl + 'Pending');
  }

  getDoctorConfirmedAppointments(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(this.baseDocUrl + 'Confirmed'); 
  }

  getPateintPendingAppointments(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(this.basePtUrl + 'Pending');
  }

  getPateintConfirmedAppointments(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(this.basePtUrl + 'Confirmed');
  }

  confirmDoctorAppointment(dto: any): Observable<IApiResponse> {
    return this._httpClient.put<IApiResponse>(this.baseDocUrl + 'Action', dto);
  }

  confirmPatientAppointment(dto: any): Observable<IApiResponse> {
    return this._httpClient.put<IApiResponse>(this.basePtUrl + 'Action', dto);
  }

  reserveAppointment(dto: any): Observable<IApiResponse> {
    return this._httpClient.post<IApiResponse>(this.baseUrl + 'Reserve', dto);
  }
}
