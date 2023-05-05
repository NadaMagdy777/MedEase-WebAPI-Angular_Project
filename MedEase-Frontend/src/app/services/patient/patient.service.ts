import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { MedicalHistory } from 'src/app/sharedClassesAndTypes/patient/medicalHistory';
import { Patient } from 'src/app/sharedClassesAndTypes/patient/patient';
import { PatientEdit } from 'src/app/sharedClassesAndTypes/patient/patientEdit';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  errorMessage: any;

  private _url:string=`${environment.apiUrl}/Patient`;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  
  constructor(private http:HttpClient) { }

  GetPatientById(id:number):Observable<Patient>
  {
    return this.http.get<Patient>(`${this._url}/id?ID=${id}`).pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }

  UpdatePatientInfo(id: number, patient:any):Observable<PatientEdit>{
    return this.http.put<PatientEdit>(
      `${this._url}?id=${id}`, 
      JSON.stringify(patient),
      this.httpOptions)
      
        .pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
    }));
  }

  AddMedicalHistory(id:number, medicalHistory:any):Observable<MedicalHistory>{
    return this.http.post<MedicalHistory>(
      `${this._url}/MedicalHistor?PatientID=${id}`,
      JSON.stringify(medicalHistory),
      this.httpOptions
    )
    
    .pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
    }));
  }

  UpdateMedicalHistory(id:number, medicalHistory:MedicalHistory):Observable<MedicalHistory>{
    return this.http.put<MedicalHistory>(
      `${this._url}/MedicalHistor?PatientID=${id}`, /////////
      JSON.stringify(medicalHistory),
      this.httpOptions
    )
    
    .pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
    }));
  }
  
}
