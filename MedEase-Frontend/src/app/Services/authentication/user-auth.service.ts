import { Router } from '@angular/router';
import { IDoctorRegisterDto } from './../../sharedClassesAndTypes/doctor/idoctor-register-dto';
import { IUserRegisterDto } from './../../SharedClassesAndTypes/patient/iuser-register-dto';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  private isLogged = new BehaviorSubject<boolean>(this.isUserLogged);
  private userName = new BehaviorSubject<string>(this.getLoggedUserName);
  loggedUser: any;
  baseUrl: string = environment.apiUrl + '/Account';
  constructor(private _httpClient: HttpClient, private _router: Router) {}

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

  registerPatient(dto: any): Observable<IApiResponse> {
    let patientUser: IUserRegisterDto = this.getUser(dto);

    return this._httpClient.post<IApiResponse>(
      this.baseUrl + '/Patient/register',
      patientUser
    );
  }

  registerDoctor(dto: any): Observable<IApiResponse> {
    let user: IUserRegisterDto = this.getUser(dto);
    let docUser: IDoctorRegisterDto = this.getDoctor(user, dto);

    console.log(docUser);

    return this._httpClient.post<IApiResponse>(
      this.baseUrl + '/Doctor/register',
      docUser
    );
  }

  saveUserData(): void {
    let encodedToken = JSON.stringify(localStorage.getItem('token'));
    let userData = jwtDecode(encodedToken);
    this.loggedUser = userData;
  }

  confirmUserLogin(token: any) {
    localStorage.setItem('token', token);
    this.saveUserData();
    this.isLogged.next(true);
    this.userName.next(this.getLoggedUserName);
    this._router.navigate(['/Home']);
  }

  get getLoggedUserName(): string {
    if (!this.loggedUser && this.isUserLogged) {
      this.saveUserData();
    }
    return this.loggedUser?.unique_name ?? '';
  }

  get getLoggedUserEmail(): string {
    if (!this.loggedUser && this.isUserLogged) {
      this.saveUserData();
    }
    return this.loggedUser?.email;
  }

  get getLoggedUserRole(): string {
    if (!this.loggedUser && this.isUserLogged) {
      this.saveUserData();
    }
    return this.loggedUser?.role;
  }

  get getLoggedUserId(): string {
    if (!this.loggedUser && this.isUserLogged) {
      this.saveUserData();
    }
    return this.loggedUser?.nameid;
  }

  get isUserLogged(): boolean {
    return localStorage.getItem('token') ? true : false;
  }

  getLoggedStatus(): Observable<boolean> {
    return this.isLogged.asObservable();
  }

  getLoggedUserNameAsObservable(): Observable<string> {
    return this.userName.asObservable();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  private getUser(dto: any): IUserRegisterDto {
    let user: IUserRegisterDto = {
      email: dto.email,
      password: dto.password,
      firstName: dto.firstName,
      lastName: dto.lastName,
      ssn: dto.ssn,
      phoneNumber: dto.phoneNumber,
      birthDate: dto.birthDate,
      addressID: dto.addressID,
      gender: Number.parseInt(dto.gender),
      building: dto.building,
      street: dto.street,
    };

    return user;
  }

  private getDoctor(user: IUserRegisterDto, dto: any): IDoctorRegisterDto {
    let docUser: IDoctorRegisterDto = {
      ...user,
      specialityID: dto.specialityID,
      allowVisa: dto.allowVisa,
      fees: dto.fees,
      faculty: dto.faculty,
      subSpecialities: dto.subSpecialities ?? [],
      insurances: dto.insurances ?? [],
      licenseImg: dto?.licenseImgTemp,
      profilePicture: dto?.profilePictureTemp,
    };

    return docUser;
  }
}
