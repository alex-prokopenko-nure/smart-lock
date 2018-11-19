import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Lock } from 'src/app/shared-module';

@Component({
  selector: 'app-info-edit',
  templateUrl: './info-edit.component.html',
  styleUrls: ['./info-edit.component.css']
})
export class InfoEditComponent implements OnInit {
  lock: Lock;
  lockInfo: string;

  constructor(
    private dialogRef: MatDialogRef<InfoEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) {
    this.lock = data.lock;
    this.lockInfo = this.lock.info;
  }

  ngOnInit() {
  }

  edit = (lockInfo: string) => {
    this.dialogRef.close(lockInfo);
  }

  close = () => {
    this.dialogRef.close();
  }
}
