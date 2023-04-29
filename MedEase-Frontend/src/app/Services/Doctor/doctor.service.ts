import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Doctor } from 'src/app/SharedClassesAndTypes/Doctor/Doctor';

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
}
