import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserAuthService } from 'src/app/services/authentication/user-auth.service';
import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnDestroy {
  constructor(
    private _userAuthService: UserAuthService,
    private _router: Router
  ) {}

  ngOnDestroy(): void {
    this.allSubscriptions.forEach((sub) => sub.unsubscribe());
  }

  allSubscriptions: Subscription[] = [];
  isLoading: boolean = false;
  serverErrorMsg: any = '';

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });

  onLoginSubmit() {
    this.isLoading = true;
    this.serverErrorMsg = '';
    this.allSubscriptions.push(
      this._userAuthService.login(this.loginForm.value).subscribe((res) => {
        if (res.success) {
          localStorage.setItem('token', res.data.token);
          this._userAuthService.saveUserData();
          this._userAuthService.isLogged.next(true);
          this._router.navigate(['/Home']);

          console.log(this._userAuthService.getLoggedUserName);
          console.log(this._userAuthService.getLoggedUserId);
          console.log(this._userAuthService.getLoggedUserRole);
          console.log(this._userAuthService.getLoggedUserEmail);
          
        } else {
          this.serverErrorMsg = res.message;
          console.log(res);
        }
        this.isLoading = false;
      })
    );
  }
}
