import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IApiResponse } from 'src/app/SharedClassesAndTypes/iapi-response';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  isLogged = new BehaviorSubject<boolean>(this.isUserLogged);
  loggedUser: any;
  baseUrl: string = environment.apiUrl + '/Account';
  constructor(private _httpClient: HttpClient) {}

  login(userLogins: object): Observable<IApiResponse> {
    return this._httpClient.post<IApiResponse>(
      this.baseUrl + '/login',
      userLogins
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.isLogged.next(false);
  }

  saveUserData(): void {
    let encodedToken = JSON.stringify(localStorage.getItem('token'));
    let userData = jwtDecode(encodedToken);
    this.loggedUser = userData;
  }

  get getLoggedUserName(): string {
    return this.loggedUser?.unique_name ?? '';
  }

  get getLoggedUserEmail(): string {
    return this.loggedUser?.email ?? '';
  }

  get getLoggedUserRole(): string {
    return this.loggedUser?.role ?? '';
  }

  get getLoggedUserId(): string {
    return this.loggedUser?.nameid ?? '';
  }

  get isUserLogged(): boolean {
    return localStorage.getItem('token') ? true : false;
  }

  getLoggedStatus(): Observable<boolean> {
    return this.isLogged.asObservable();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
