import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { UnauthGuardService } from './guards/unauth.guard';
import { AuthGuardService } from './guards/auth.guard';

const routes: Routes = [
  {
    path: "login",
    component: LoginComponent,
    canActivate: [UnauthGuardService]
  },
  {
    path: "register",
    component: RegisterComponent,
    canActivate: [UnauthGuardService]
  },
  {
    path: "home",
    component: HomeComponent,
    canActivate: [AuthGuardService]
  },
  { path: "**", redirectTo: "home" },
  { path: "", redirectTo: "home" }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SmartLockRoutingModule { }