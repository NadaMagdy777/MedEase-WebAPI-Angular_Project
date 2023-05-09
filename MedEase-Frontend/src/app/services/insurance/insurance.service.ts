import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';
import { Insurance } from 'src/app/sharedClassesAndTypes/patient/insurance';
import { IInsurance } from 'src/app/sharedClassesAndTypes/shared/iinsurance';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InsuranceService {

  _url: string = environment.apiUrl + '/General/Insurance';

  constructor(private _httpClient: HttpClient) {}

  getInsurances(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(
      this._url
    );
  }

  GetCommonInsurancesPatient_Doctor(docId:number, patientId:number):Observable<IInsurance>{
    return this._httpClient.get<IInsurance>(`${this._url}/${docId}/${patientId}`)
    .pipe(catchError((err)=>{
      return throwError(()=>err.message ||"server error");
      
    }));
  }
}
