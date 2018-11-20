import { Component, OnInit, OnDestroy } from "@angular/core";
import { AuthService } from "src/app/shared-module/services/auth.service";
import { MatSnackBar, MatDialog } from "@angular/material";
import {
  LockRent,
  User,
  UserRole,
  SmartLockApiService,
  LockRentRights,
  Lock
} from "src/app/shared-module";
import { LocksService } from "../services/locks.service";
import { DeleteDialogComponent } from "../delete-dialog/delete-dialog.component";
import { ActionStatus } from "../enums/action-status.enum";
import { OperationsComponent } from "../operations/operations.component";
import { RentersComponent } from "../renters/renters.component";
import { InfoEditComponent } from "../info-edit/info-edit.component";
import { WebsocketService } from "../services/websocket.service";
import { Subscription } from "rxjs";
import { LanguageService } from "../services/language.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit, OnDestroy {
  lockRents: LockRent[];
  fullName: string;
  user: User;
  userId: number;
  isDownloaded: boolean = false;
  lockOperationsSubscription: Subscription;

  constructor(
    private authService: AuthService,
    private locksService: LocksService,
    public snackBar: MatSnackBar,
    private dialog: MatDialog,
    private webSocketService: WebsocketService,
    private languageService: LanguageService
  ) {
    this.lockOperationsSubscription = webSocketService.lockOperationsSubject.subscribe(
      event => {
        this.setState(event.lockId, event.locked);
      }
    );
    this.userId = authService.currentUserId;
    authService.getUserInfo().subscribe(result => {
      this.user = result;
      this.fullName = result.firstName + " " + result.lastName;
      setTimeout(
        x =>
          snackBar.open(languageService.getHello() + this.fullName, languageService.getGreetings(), {
            duration: 3000
          }),
        1000
      );
    });
    this.locksService.getUserRents(this.userId).subscribe(
      result => {
        this.lockRents = result;
        this.isDownloaded = true;
      },
      error => {
        this.isDownloaded = true;
      }
    );
  }

  get UserRole() {
    return UserRole;
  }

  get LockRentRights() {
    return LockRentRights;
  }

  ngOnInit() {}

  ngOnDestroy(): void {
    this.lockOperationsSubscription.unsubscribe();
  }

  setState = (lockId: number, locked: boolean) => {
    this.lockRents.forEach(element => {
      if (element.lockId == lockId) {
        element.lock.locked = locked;
        this.snackBar.open(
          this.languageService.getLockWithId() +
            lockId +
            this.languageService.getHasBeen() +
            (locked
              ? this.languageService.getLocked()
              : this.languageService.getOpened()),
          this.languageService.getNotification(),
          { duration: 3000 }
        );
      }
    });
  };

  addLock = () => {
    this.locksService.addLock().subscribe(
      result => {
        this.locksService.getUserRents(this.userId).subscribe(result => {
          this.lockRents = result;
          this.isDownloaded = true;
        });
      },
      error => {
        this.snackBar.open(this.languageService.getFailed(), this.languageService.getError(), { duration: 5000 });
      }
    );
  };

  deleteLock = (lockId: number) => {
    const dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result == ActionStatus.Success) {
        this.locksService.deleteLock(lockId).subscribe(result => {
          this.locksService.getUserRents(this.userId).subscribe(result => {
            this.lockRents = result;
          });
        });
      }
    });
  };

  showOperations = (lockId: number) => {
    const dialogRef = this.dialog.open(OperationsComponent, {
      data: { userId: this.userId, lockId: lockId }
    });
  };

  showRenters = (lockRent: LockRent) => {
    const dialogRef = this.dialog.open(RentersComponent, {
      data: {
        rights: lockRent.rights,
        lockId: lockRent.lockId,
        userId: this.userId
      }
    });
  };

  editInfo = (lock: Lock) => {
    const dialogRef = this.dialog.open(InfoEditComponent, {
      data: { lock: lock }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let newLock = new Lock();
        newLock.info = result;
        this.locksService
          .updateLockInfo(lock.id, newLock)
          .subscribe(response => {
            this.locksService.getUserRents(this.userId).subscribe(result => {
              this.lockRents = result;
            });
          });
      }
    });
  };
}
