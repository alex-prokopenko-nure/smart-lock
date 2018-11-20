import { Component, OnInit, Inject } from '@angular/core';
import { LocksService } from '../services/locks.service';
import { MAT_DIALOG_DATA, MatDialogRef, MatTableDataSource } from '@angular/material';
import { User, LockRent, LockRentRights, UserRole, ShareRightsViewModel } from 'src/app/shared-module';
import { UsersService } from '../services/users.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-renters',
  templateUrl: './renters.component.html',
  styleUrls: ['./renters.component.css']
})
//TODO: Autocomplete
export class RentersComponent implements OnInit {
  renters: LockRent[];
  users: User[] = [];
  userId: number;
  lockId: number;
  filteredUsers: User[] = [];
  tableSource: MatTableDataSource<LockRent> = new MatTableDataSource<LockRent>();
  displayedColumns: string[] = ['Name', 'Timing', 'Rights'];
  rights: LockRentRights;
  shareRightsForm: FormGroup;
  usernameControl: FormControl;

  constructor(
    private dialogRef: MatDialogRef<RentersComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private locksService: LocksService,
    private usersService: UsersService,
    builder: FormBuilder
  ) {
    this.userId = data.userId;
    this.lockId = data.lockId;
    this.rights = data.rights;
    if (this.rights == LockRentRights._1 || this.rights == LockRentRights._2) {
      this.displayedColumns.push('Actions');
    }
    locksService.getRenters(data.lockId, data.rights).subscribe(
      result => {
        this.renters = result;
        this.tableSource.data = this.renters;
      }
    );
    if (this.rights != LockRentRights._3) {
      usersService.getUsers().subscribe(
        result => {
          this.users = result;
          this.users = this.users.filter(x => x.role != UserRole._1);
        }
      );

      this.shareRightsForm = builder.group({
        rights: [3, Validators.required],
        username: ["", Validators.required],
        userId: [Validators.required]
      });
    }
  }

  get LockRentRights() {
    return LockRentRights;
  }

  ngOnInit() {
    this.shareRightsForm.controls['username'].valueChanges
      .pipe(
        startWith(''),
        map(user => user ? this.filterUsers(user) : this.users.slice())
      ).subscribe(result => {
        this.filteredUsers = result;
        this.shareRightsForm.controls['userId'].setValue(this.userExists(this.shareRightsForm.controls['username'].value));
      });
  }

  getRenters = () => {
    this.locksService.getRenters(this.lockId, this.rights).subscribe(
      renters => {
        this.renters = renters;
        this.tableSource.data = renters;
        this.tableSource._updateChangeSubscription();
      }
    );
  }

  cancelRent = (rent: LockRent) => {
    this.locksService.cancelRent(rent.lockId, rent.userId).subscribe(
      result => {
        this.getRenters();
      }
    );
  }

  shareRights = () => {
    if (this.shareRightsForm.valid) {
      let value = this.shareRightsForm.value;
      let model = new ShareRightsViewModel();
      model.ownerId = value.userId;
      model.adminId = this.userId;
      model.lockId = this.lockId;
      model.from = new Date(Date.now());
      model.rights = +value.rights;
      this.locksService.shareRights(model).subscribe(
        result => {
          this.getRenters();
        }
      );
    }
  }

  getRights = (element: LockRent) => {
    if (element.rights == LockRentRights._1) {
      return "Admin";
    } else if (element.rights == LockRentRights._2) {
      return "Owner";
    } else if (element.rights == LockRentRights._3) {
      return "Renter";
    }
  }

  userExists = (username: string) => {
    let user = this.users.find(x => x.username == username);
    return user ? user.id : user;
  }

  filterUsers(username: string) {
    return this.users.filter(option =>
      option.username.toLowerCase()
      .indexOf(username.toLowerCase()) === 0);
  }

  chooseUser(user: User) {
    this.shareRightsForm.controls['username'].setValue(user.username);
  }

  getName = (rent: LockRent) => {
    return rent.user.firstName + " " + rent.user.lastName;
  }
}
