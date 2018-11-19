import { Injectable } from '@angular/core';
import { SmartLockApiService, LockRentRights, ShareRightsViewModel, Lock } from 'src/app/shared-module';

@Injectable()
export class LocksService {
  constructor(private smartLockApiService: SmartLockApiService) { }

  getUserRents = (userId: number) => {
    return this.smartLockApiService.apiLocksAllLocksByUserIdGet(userId);
  }

  addLock = () => {
    return this.smartLockApiService.apiLocksPost();
  }

  updateLockInfo = (lockId: number, lockModel: Lock) => {
    return this.smartLockApiService.apiLocksByLockIdPut(lockId, lockModel);
  }

  deleteLock = (lockId: number) => {
    return this.smartLockApiService.apiLocksByLockIdDelete(lockId);
  }

  getOperations = (lockId: number, userId: number) => {
    return this.smartLockApiService.apiLocksByLockIdOperationsGet(lockId, userId);
  }

  getRenters = (lockId: number, rights: LockRentRights) => {
    return this.smartLockApiService.apiLocksByLockIdRentersGet(lockId, rights);
  }

  shareRights = (model: ShareRightsViewModel) => {
    return this.smartLockApiService.apiLocksShareRightsPost(model);
  }

  cancelRent = (lockId: number, userId: number) => {
    return this.smartLockApiService.apiLocksByLockIdCancelByUserIdDelete(lockId, userId);
  }
}
