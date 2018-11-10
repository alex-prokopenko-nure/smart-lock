import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared-module/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    iconRegistry: MatIconRegistry,
    sanitizer: DomSanitizer,
    private authService: AuthService,
    private router: Router
    ) { 
      iconRegistry.addSvgIcon(
        "padlock",
        sanitizer.bypassSecurityTrustResourceUrl("assets/img/padlock.svg")
      );
    }

  ngOnInit() {
  }

  isLoggedIn = () => this.authService.isLoggedIn()

  logout = () => this.authService.logout()

  navigateToHome = () => this.router.navigateByUrl("/smartlock/home");

  navigateToLogin = () => this.router.navigateByUrl("/smartlock/login");

  navigateToRegister = () => this.router.navigateByUrl("/smartlock/register");
}