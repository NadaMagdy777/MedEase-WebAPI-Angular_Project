import { IDoctorRegisterDto } from './../../SharedClassesAndTypes/Doctor/idoctor-register-dto';
import { IUserRegisterDto } from './../../SharedClassesAndTypes/patient/iuser-register-dto';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IApiResponse } from 'src/app/sharedClassesAndTypes/iapi-response';
import jwtDecode from 'jwt-decode';
import { Gender } from 'src/app/SharedClassesAndTypes/enums/gender';
import * as buffer from 'buffer';

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

  registerPatient(dto: any): Observable<IApiResponse> {
    let patientUser: IUserRegisterDto = this.getUser(dto);

    return this._httpClient.post<IApiResponse>(
      this.baseUrl + '/Patient/register',
      patientUser
    );
  }

  registerDoctor(dto: any): Observable<IApiResponse> {
    console.log("registerDoctor");
    console.log(dto);

    let user: IUserRegisterDto = this.getUser(dto);
    let docUser: IDoctorRegisterDto = this.getDoctor(user, dto);
    console.log(docUser);

    let subspecsarr = '';
    docUser.subSpecialities?.forEach
    (element => subspecsarr += element.toString() +',' );

    let insurancesarr = '';
    docUser.insurances?.forEach(element => insurancesarr += element.toString() +',' );

    let formData = new FormData();
    formData.append('email', user.email);
    formData.append('password', user.password);
    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('ssn', user.ssn);
    formData.append('phoneNumber', user.phoneNumber);
    formData.append('gender', user.gender.toString());
    formData.append('birthDate', user.birthDate.toString());
    formData.append('building', user.building.toString());
    formData.append('street', user.street.toString());
    formData.append('addressID', user.addressID.toString());
    formData.append('allowVisa', docUser.allowVisa.toString());
    formData.append('fees', docUser.fees.toString());
    formData.append('faculty', docUser.faculty.toString());
    formData.append('specialityIDTemp', docUser.specialityIDTemp.toString());
    formData.append('subSpecialities', JSON.stringify(subspecsarr));
    formData.append('insurances', JSON.stringify(insurancesarr));
    formData.append('licenseImg', docUser.licenseImgForm);
    formData.append('profilePicture', docUser.profilePictureForm);

    console.log(docUser.specialityIDTemp.toString());
    console.log(docUser.profilePictureForm);
    
    return this._httpClient.post<IApiResponse>(
      this.baseUrl + '/Doctor/register',
      //docUser
      //{formData, ...docUser},
      formData
    );
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

  private getUser(dto: any): IUserRegisterDto {
    console.log("getUser");
    console.log(dto);
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
    console.log("getDoctor");
    console.log(dto);
    let docUser: IDoctorRegisterDto = {
      ...user,
      specialityIDTemp: dto.specialityID,
      allowVisa: dto.allowVisa,
      fees: dto.fees,
      faculty: dto.faculty,
      subSpecialities: dto.subSpecialities ?? [],
      insurances: dto.insurances ?? [],
      // licenseImg: dto.licenseImg,
      // profilePicture: dto.profilePicture,
      // licenseImg: new Uint8Array(licenseImgArrayBuffer),
      // profilePicture: new Uint8Array(profilePictureArrayBuffer),
      licenseImgForm : dto?.licenseImg,
      profilePictureForm : dto?.profilePicture,
    };

    return docUser;
  }
}
