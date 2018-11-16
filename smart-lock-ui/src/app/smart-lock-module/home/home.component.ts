import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { MatSnackBar, MatDialog } from '@angular/material';
import { LockRent, User, UserRole, SmartLockApiService, LockRentRights } from 'src/app/shared-module';
import { LocksService } from '../services/locks.service';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { ActionStatus } from '../enums/action-status.enum';
import { OperationsComponent } from '../operations/operations.component';
import { RentersComponent } from '../renters/renters.component';

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
    public snackBar: MatSnackBar,
    private dialog: MatDialog
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

  get LockRentRights() {
    return LockRentRights;
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

  deleteLock = (lockId: number) => {
    const dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result == ActionStatus.Success) {
        this.locksService.deleteLock(lockId).subscribe(
          result => {
            this.locksService.getUserRents(this.userId).subscribe(
              result => {
                this.lockRents = result
              }
            );
          }
        );
      }
    });
  }

  showOperations = (lockId: number) => {
    const dialogRef = this.dialog.open(OperationsComponent, {
      data: {userId: this.userId, lockId: lockId}
    });
  }

  showRenters = (lockRent: LockRent) => {
    const dialogRef = this.dialog.open(RentersComponent, {
      data: {rights: lockRent.rights, lockId: lockRent.lockId, userId: this.userId}
    });
  }
}
