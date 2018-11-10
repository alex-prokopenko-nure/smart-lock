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
      setTimeout(x => snackBar.open("Hello, " + this.fullName, "Greetings", {duration: 3000}), 1000);
    });
    this.locksService.getUserRents(this.userId).subscribe(result => {
      this.lockRents = result;
      this.isDownloaded = true;
    }, error => {
      this.isDownloaded = true;
    });
  }

  get UserRole() {
    return UserRole;
  }

  ngOnInit() {
  }

  addLock = () => {
    this.locksService.addLock().subscribe(
      result => {
        this.locksService.getUserRents(this.userId).subscribe(result => {
          this.lockRents = result;
          this.isDownloaded = true;
        });
      },
      error => {
        this.snackBar.open("Lock addition failed", "Error", {duration: 5000});
      }
    );
  }
}
