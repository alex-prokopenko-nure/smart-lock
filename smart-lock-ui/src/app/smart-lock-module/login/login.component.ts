import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginViewModel } from 'src/app/shared-module';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { ActionStatus } from '../enums/action-status.enum';
import { Router, Params, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, AfterViewInit {
  loginForm: FormGroup;
  loginViewModel: LoginViewModel = new LoginViewModel();

  constructor(
    builder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    public snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private translate: TranslateService
    ) {
      this.loginForm = builder.group({
        email: ["", Validators.compose([Validators.required, Validators.email])],
        password: ["", Validators.compose([Validators.required, Validators.minLength(6)])]
      });
  }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      if (params['registered']) {
        this.snackBar.open("You have registered successfuly!", "Notification", {duration: 5000});
      }
    });
  }

  login = async () => {
    if (this.loginForm.valid) {
      this.loginViewModel.email = this.loginForm.value.email;
      this.loginViewModel.password = this.loginForm.value.password;
      let status: ActionStatus = await this.authService.login(this.loginViewModel);
      if (status == ActionStatus.Success) {
        this.router.navigateByUrl("smartlock/home");
      } else if (status == ActionStatus.Failed) {
        this.snackBar.open("Wrong login or password!", "Error", {duration: 5000});
      }
    }
  }

}
