import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Doctor } from 'src/app/SharedClassesAndTypes/Doctor/Doctor';
import { IreserveAppointement } from 'src/app/SharedClassesAndTypes/Doctor/IReserveAppointement';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  _url: string = environment.apiUrl + '/Doctor';
  errorMessage: any;
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

}
