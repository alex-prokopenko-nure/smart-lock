import { Injectable } from '@angular/core';
import { SmartLockApiService, LockRentRights } from 'src/app/shared-module';

@Injectable()
export class UsersService {
  constructor(private smartLockApiService: SmartLockApiService) { }

  getUsers = () => {
    return this.smartLockApiService.apiUsersGet();
  }
}
