import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IApiResponse } from 'src/app/SharedClassesAndTypes/iapi-response';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  loggedUserSubject = new BehaviorSubject<boolean>(this.isUserLogged);
  baseUrl: string = environment.apiUrl + '/Account';
  constructor(private _httpClient: HttpClient) {}

  login(email: string, password: string) {
    this._httpClient.post<IApiResponse>(this.baseUrl + '/login', { email, password })
  }

  logout() {
    localStorage.removeItem('token');
    this.loggedUserSubject.next(false);
  }

  get isUserLogged(): boolean {
    return localStorage.getItem('token') ? true : false;
  }

  getloggedStatus(): Observable<boolean> {
    return this.loggedUserSubject.asObservable();
  }
}
