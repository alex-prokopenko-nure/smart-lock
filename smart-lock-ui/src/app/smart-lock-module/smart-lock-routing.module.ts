import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { UnauthGuardService } from './guards/unauth.guard';
import { AuthGuardService } from './guards/auth.guard';
import { ContactsComponent } from './contacts/contacts.component';
import { SmartLockComponent } from './smart-lock.component';

const routes: Routes = [
  {
    path: '',
    component: SmartLockComponent,
    children: [
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
      {
        path: "contacts",
        component: ContactsComponent
      },
      { path: "**", 
        redirectTo: "home"
      },
      { 
        path: "", 
        redirectTo: "home"
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SmartLockRoutingModule { }