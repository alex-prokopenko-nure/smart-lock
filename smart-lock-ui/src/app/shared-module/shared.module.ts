import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { API_BASE_URL, SmartLockApiService } from './services/smart-lock.api.service';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.API_BASE_URL },
    SmartLockApiService
  ]
})
export class SharedModule { }
