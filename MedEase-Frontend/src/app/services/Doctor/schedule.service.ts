import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, throwError } from 'rxjs';
import { EditSchedule, Schedule } from 'src/app/sharedClassesAndTypes/doctor/schedule';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  errorMessage: any;

  private _url:string=`${environment.apiUrl}/Doctor`;
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  
  constructor(private http:HttpClient) { }

  GetAllDoctorSchedules(id:number):Observable<EditSchedule[]>{
    return this.http.get<EditSchedule[]>(`${this._url}/getSchedules/id?DocId=${id}`).pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }

  UpdateSchedules(id:number, schedules:any):Observable<EditSchedule>{
    return this.http.put<EditSchedule>(
      `${this._url}/Schedule/id?Id=${id}`,JSON.stringify(schedules),
      this.httpOptions)

      .pipe(catchError((err)=>{
        return throwError(()=>err.message ||"server error");
      }));
  }

  AddSchedule(schedules:any):Observable<Schedule>{
    return this.http.post<Schedule>(
      `${this._url}/schedule/new`,JSON.stringify(schedules),
      this.httpOptions)

      .pipe(catchError((err)=>{
        return throwError(()=>err.message ||"server error");
      }));
    }
}
