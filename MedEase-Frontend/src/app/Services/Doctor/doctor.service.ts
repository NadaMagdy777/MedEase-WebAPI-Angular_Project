import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from 'src/app/sharedClassesAndTypes/doctor/Doctor';
import { DoctorEdit } from 'src/app/sharedClassesAndTypes/doctor/doctorEdit';
import { IreserveAppointement } from 'src/app/sharedClassesAndTypes/doctor/IReserveAppointement';
import { IDiagnosisDto } from 'src/app/sharedClassesAndTypes/diagnosis/i-diagnosis-dto';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';
import { Review } from 'src/app/sharedClassesAndTypes/review/review';
import { Insurance } from 'src/app/sharedClassesAndTypes/patient/insurance';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  _url: string = environment.apiUrl + '/Doctor';
  errorMessage: any;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  
  constructor(private http: HttpClient) {}

  GetAllDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this._url).pipe(
      catchError((err) => {
       
        return throwError(() => err.message || 'server error');
      })
    );
  }

  
  GetTopRated(): Observable<IApiResponse> {
    return this.http.get<IApiResponse>(this._url +"/GetTopRatedDoctors").pipe(
      catchError((err) => {
       
        return throwError(() => err.message || 'server error');
      })
    );
  }
  GetDoctorByID(id:number):Observable<Doctor[]>
  {
    return this.http.get<Doctor[]>(this._url+"/id?ID="+id).pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }

  GetDoctorReviews(id:number):Observable<Doctor[]>
  {
    return this.http.get<Doctor[]>(this._url+"/Reviews?Id="+id).pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }

  UpdateDoctorInfo(id: number, doctor:any):Observable<DoctorEdit>{
    return this.http.put<DoctorEdit>(
      `${this._url}?id=${id}`, 
      JSON.stringify(doctor),
      this.httpOptions)
      
        .pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
    }));
  }

  GetDoctorAppointementAndPattern(id:number):Observable<any>
  {
    return this.http.get<any>(this._url+"/GetAppointmentsandPattern?Id="+id).pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }
  
  ReserveAppointement(data:IreserveAppointement):Observable<any>
  {
    console.log('sending',environment.apiUrl + '/Appointment/Reserve');    
    return this.http.post<any>(environment.apiUrl + '/Appointment/Reserve' ,data);
  }
  postDiagnosis(data:IDiagnosisDto) :Observable<IApiResponse> {
    return this.http.post<IApiResponse>(this._url + '/diagnosis/new', data);

  }
  postReview(data:Review):Observable<IApiResponse>{
    return this.http.post<IApiResponse>(this._url + '/Reviews', data);

  }

}
