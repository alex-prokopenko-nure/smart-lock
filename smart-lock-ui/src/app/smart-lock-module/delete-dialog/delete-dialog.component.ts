import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { ActionStatus } from '../enums/action-status.enum';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})
export class DeleteDialogComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<DeleteDialogComponent>) { }

  ngOnInit() {
  }

  delete = () => {
    this.dialogRef.close(ActionStatus.Success);
  }

  close = () => {
    this.dialogRef.close();
  }
}
