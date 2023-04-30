import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';
import { PatientEdit } from 'src/app/sharedClassesAndTypes/patient/patientEdit';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  errorMessage: any;

  private _url:string='Patient';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  
  constructor(private http:HttpClient) { }

  GetPatientById(id:number):Observable<Patient>
  {
    return this.http.get<Patient>(`${environment.apiUrl}/${this._url}/id?ID=${id}`).pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }

  UpdatePatientInfo(id: number, patient:any):Observable<PatientEdit>{
    console.log(patient);
    return this.http.put<PatientEdit>(
      `${environment.apiUrl}/${this._url}?id=${id}`, 
      JSON.stringify(patient),
      this.httpOptions)
      
        .pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
    }));
  }
}
