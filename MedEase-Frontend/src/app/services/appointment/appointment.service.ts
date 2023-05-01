import { IApiResponse } from './../../SharedClassesAndTypes/iapi-response';
import { environment } from './../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor(private _httpClient: HttpClient) {}

  // /////
  // //For Test Only (Remove Later)
  // ////
  // headers = new HttpHeaders({
  //   Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJuYW1laWQiOiI5IiwidW5pcXVlX25hbWUiOiJBYmRhbGxhaCBBc3Nha2VyIiwicm9sZSI6IkRvY3RvciIsIm5iZiI6MTY4MjcyNDQwNywiZXhwIjoxNjgzMzI5MjA3LCJpYXQiOjE2ODI3MjQ0MDcsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MTc2ODUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.CjVh4TYq8U7IR5E_oSLpuhRBLkMO1v_O47TdFyyvDzc`
  // });

  // options = {
  //   headers: this.headers
  // };
  // /////
  // //For Test Only (Remove Later)
  // ////

  baseUrl: string = environment.apiUrl + '/';
  baseDocUrl: string = environment.apiUrl + '/Appointment/Doctor/';
  basePtUrl: string = environment.apiUrl + '/Appointment/Patient/';

  getDoctorPendingAppointments(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(this.baseDocUrl + 'Pending'); //, this.options);           //remove this.options
  }

  getDoctorConfirmedAppointments(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(this.baseDocUrl + 'Confirmed'); //, this.options);       //remove this.options
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
