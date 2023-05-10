import { Subscription } from 'rxjs';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  constructor(private _userAuthService: UserAuthService,private router:Router) {}

  allSubscriptions: Subscription[] = [];
  isUserLogged: boolean = this._userAuthService.isUserLogged;
  loggedUserName: string = this._userAuthService.getLoggedUserName;

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._userAuthService.getLoggedStatus().subscribe((res) => {
        this.isUserLogged = res;
      })
    );

    this.allSubscriptions.push(
      this._userAuthService.getLoggedUserNameAsObservable().subscribe((res) => {
        this.loggedUserName = res;
      })
    );
  }

  ngOnDestroy(): void {
    this.allSubscriptions.forEach((sub) => sub.unsubscribe());
  }

  onLogOut(): void {
    this._userAuthService.logout();
    this.router.navigate(["/Home"])
  }

  currentUserRole(): string {
    return this._userAuthService.getLoggedUserRole;
  }
}
