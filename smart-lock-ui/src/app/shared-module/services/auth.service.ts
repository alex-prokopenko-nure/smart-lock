import { Injectable } from '@angular/core';
import { SmartLockApiService } from 'src/app/shared-module';
import * as jwt_decode from 'jwt-decode';
import { LoginViewModel, RegisterViewModel } from './smart-lock.api.service';
import { Router } from '@angular/router';
import { ActionStatus } from 'src/app/smart-lock-module/enums/action-status.enum';

@Injectable()
export class AuthService {
  TOKEN_NAME: string = "smartlock_token";
  currentUserId: number;

  constructor(
    private smartLockApiService: SmartLockApiService,
    private router: Router
    ) { 
      if (this.isLoggedIn()) {
        this.getInfo();
      }
    }

  login = async (model: LoginViewModel) => {
    let status: ActionStatus;
    await this.smartLockApiService.apiUsersLoginPost(
      model
    ).toPromise().then(token => {
        localStorage.setItem(this.TOKEN_NAME, token);
        const decoded = jwt_decode(token);
        this.currentUserId = +decoded["sub"];
        status = ActionStatus.Success;
      },
      error => {
        status = ActionStatus.Failed;
      }
    );

    return status;
  }

  getInfo = () => {
    let token = localStorage.getItem(this.TOKEN_NAME);
    const decoded = jwt_decode(token);
    this.currentUserId = +decoded["sub"];
  }

  isLoggedIn = () => {
    return Date.now() < this.getExpirationTime();
  }

  getExpirationTime = () => {
    const token = localStorage.getItem(this.TOKEN_NAME);

    if (token) {
      const decoded = jwt_decode(token);
      const expirationTime = decoded["exp"] * 1000;
      return expirationTime;
    }

    return 0;
  }

  logout = () => {
    localStorage.removeItem(this.TOKEN_NAME);
    this.router.navigateByUrl("smartlock/login");
  }

  register = async (model: RegisterViewModel) => {
    let status: ActionStatus;
    await this.smartLockApiService.apiUsersRegisterPost(
      model
    ).toPromise().then(() => {
        status = ActionStatus.Success;
      },
      error => {
        status = ActionStatus.Failed;
      }
    );

    return status;
  }

  getUserInfo = () => {
    return this.smartLockApiService.apiUsersByUserIdInfoPost(this.currentUserId);
  }
}
