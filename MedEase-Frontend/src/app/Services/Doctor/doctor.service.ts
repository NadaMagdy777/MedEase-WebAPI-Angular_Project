import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from 'src/app/sharedClassesAndTypes/doctor/doctor';
import { DoctorEdit } from 'src/app/sharedClassesAndTypes/doctor/doctorEdit';

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
  GetDoctorByID(id:number):Observable<Doctor[]>
  {
    return this.http.get<Doctor[]>(this._url+"/id?ID="+id).pipe(catchError((err)=>{
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
  
}
