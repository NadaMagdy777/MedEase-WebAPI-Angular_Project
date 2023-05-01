import { Subscription } from 'rxjs';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  constructor(private _userAuthService: UserAuthService) {}

  allSubscriptions: Subscription[] = [];
  isUserLogged: boolean = this._userAuthService.isUserLogged;

  ngOnInit(): void {
    this.allSubscriptions.push(
      this._userAuthService.getLoggedStatus().subscribe((res) => {
        this.isUserLogged = res;
      })
    );
  }

  ngOnDestroy(): void {
    this.allSubscriptions.forEach((sub) => sub.unsubscribe());
  }

  onLogOut():void{
    this._userAuthService.logout();
  }
}
