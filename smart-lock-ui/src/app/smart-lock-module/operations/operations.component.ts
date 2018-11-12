import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatTableDataSource } from '@angular/material';
import { LocksService } from '../services/locks.service';
import { LockOperation, LockOperationState } from 'src/app/shared-module';

@Component({
  selector: 'app-operations',
  templateUrl: './operations.component.html',
  styleUrls: ['./operations.component.css']
})
export class OperationsComponent implements OnInit {
  lockOperations: LockOperation[];
  tableSource: MatTableDataSource<LockOperation>;
  displayedColumns: string[] = ['Operation', 'Time'];

  constructor(
    private dialogRef: MatDialogRef<OperationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private locksService: LocksService
    ) {
      locksService.getOperations(data.lockId, data.userId).subscribe(
        result => {
          this.lockOperations = result;
          this.tableSource = new MatTableDataSource<LockOperation>(this.lockOperations);
        }
      );
  }

  get LockOperationState() { 
    return LockOperationState;
  }

  ngOnInit() {
  }

  getState = (state: LockOperationState) => {
    if (state == LockOperationState._1) {
      return "Opened";
    }
    if (state == LockOperationState._2) {
      return "Closed";
    }
    if (state == LockOperationState._3) {
      return "Open failed";
    }
  }
}
