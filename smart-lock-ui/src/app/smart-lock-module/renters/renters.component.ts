import { Component, OnInit, Inject } from '@angular/core';
import { LocksService } from '../services/locks.service';
import { MAT_DIALOG_DATA, MatDialogRef, MatTableDataSource } from '@angular/material';
import { User, LockRent, LockRentRights, UserRole } from 'src/app/shared-module';
import { UsersService } from '../services/users.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-renters',
  templateUrl: './renters.component.html',
  styleUrls: ['./renters.component.css']
})
//TODO: Autocomplete
export class RentersComponent implements OnInit {
  renters: LockRent[];
  users: User[];
  tableSource: MatTableDataSource<LockRent>;
  displayedColumns: string[] = ['Name', 'Timing', 'Rights'];
  rights: LockRentRights;
  shareRightsForm: FormGroup

  constructor(
    private dialogRef: MatDialogRef<RentersComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private locksService: LocksService,
    private usersService: UsersService,
    builder: FormBuilder
  ) {
    this.rights = data.rights;
    locksService.getRenters(data.lockId, data.rights).subscribe(
      result => {
        this.renters = result;
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
        rights: [Validators.required],
        userId: [Validators.required]
      });
    }
  }

  get LockRentRights() {
    return LockRentRights;
  }

  ngOnInit() {
  }

  getName = (rent: LockRent) => {
    return rent.user.firstName + " " + rent.user.lastName;
  }

  getRights = (rent: LockRent) => {
    return rent.rights == LockRentRights._2 ? "Owner" : "Renter"; 
  }
}
