import { Injectable } from '@angular/core';
import { SmartLockApiService } from 'src/app/shared-module';

@Injectable()
export class AuthService {

  constructor(private smartLockApiService: SmartLockApiService) { }
}
