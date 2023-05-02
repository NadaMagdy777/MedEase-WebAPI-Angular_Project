import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class InsuranceService {
  constructor(private _httpClient: HttpClient) {}

  getInsurances(): Observable<IApiResponse> {
    return this._httpClient.get<IApiResponse>(
      environment.apiUrl + '/General/Insurance'
    );
  }
}
