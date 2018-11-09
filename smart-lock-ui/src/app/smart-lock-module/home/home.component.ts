import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { MatSnackBar } from '@angular/material';
import { LockRent, User, UserRole } from 'src/app/shared-module';
import { LocksService } from '../services/locks.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  lockRents: LockRent[];
  fullName: string;
  user: User;
  userId: number;
  isDownloaded: boolean = false;

  constructor(
    private authService: AuthService,
    private locksService: LocksService,
    public snackBar: MatSnackBar
  ) { 
    this.userId = authService.currentUserId;
    authService.getUserInfo().subscribe(result => {
      this.user = result;
      this.fullName = result.firstName + " " + result.lastName;
    });
    locksService.getUserRents(this.userId).subscribe(result => {
      this.lockRents = result;
      this.isDownloaded = true;
    });
  }

  get UserRole() {
    return UserRole;
  }

  ngOnInit() {
  }

}
