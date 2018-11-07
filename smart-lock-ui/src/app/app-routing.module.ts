import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: "smartlock",
    loadChildren: "./smart-lock-module/smart-lock.module#SmartLockModule"
  },
  { path: "**", redirectTo: "smartlock" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
