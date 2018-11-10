import { Injectable } from '@angular/core';
import { SmartLockApiService } from 'src/app/shared-module';

@Injectable()
export class LocksService {
  constructor(private smartLockApiService: SmartLockApiService) { }

  getUserRents = (userId: number) => {
    return this.smartLockApiService.apiLocksAllLocksByUserIdGet(userId);
  }

  addLock = () => {
    return this.smartLockApiService.apiLocksPost();
  }
}
