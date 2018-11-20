import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared-module';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthService } from './shared-module/services/auth.service';
import { AuthGuardService } from './smart-lock-module/guards/auth.guard';
import { UnauthGuardService } from './smart-lock-module/guards/unauth.guard';
import { WebsocketService } from './smart-lock-module/services/websocket.service';
import { MatIconModule } from '@angular/material';
import { LanguageService } from './smart-lock-module/services/language.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatIconModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthService,
    AuthGuardService,
    UnauthGuardService,
    WebsocketService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }