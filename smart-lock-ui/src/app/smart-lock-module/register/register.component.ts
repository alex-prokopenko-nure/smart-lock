import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RegisterViewModel } from 'src/app/shared-module';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { Router } from '@angular/router';
import { ActionStatus } from '../enums/action-status.enum';
import { MatSnackBar } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../services/language.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  registerViewModel: RegisterViewModel = new RegisterViewModel();
  showError: boolean = false;

  constructor(
    builder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    public snackBar: MatSnackBar
    ) {
    this.registerForm = builder.group({
      email: ["", Validators.compose([Validators.required, Validators.email])],
      password: ["", Validators.compose([Validators.required, Validators.minLength(6)])],
      firstName: ["", Validators.required],
      lastName: ["", Validators.required],
      username: ["", Validators.required]     
    });
  }

  ngOnInit() {
  }

  register = async () => {
    this.showError = false;
    if (this.registerForm.valid) {
      this.registerViewModel.email = this.registerForm.value.email;
      this.registerViewModel.password = this.registerForm.value.password;
      this.registerViewModel.firstName = this.registerForm.value.firstName;
      this.registerViewModel.lastName = this.registerForm.value.lastName;
      this.registerViewModel.username = this.registerForm.value.username;
      let status: ActionStatus = await this.authService.register(this.registerViewModel);
      if (status == ActionStatus.Success) {
        this.router.navigateByUrl("smartlock/login?registered=1");
      } else if (status == ActionStatus.Failed) {
        this.snackBar.open("This email already has an account!", "Error", {duration: 5000});
      }
    }
  }
}
