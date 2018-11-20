import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { API_BASE_URL, SmartLockApiService } from './services/smart-lock.api.service';
import { environment } from 'src/environments/environment';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
      }
    })
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.API_BASE_URL },
    SmartLockApiService
  ],
  exports: [
    TranslateModule
  ]
})
export class SharedModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
