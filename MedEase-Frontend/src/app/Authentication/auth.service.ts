import { Injectable } from '@angular/core';
import { UserAuthService } from '../services/authentication/user-auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private userService:UserAuthService) { }
  
  isAuthenticated(){
    return this.userService.isUserLogged;
  }
}
