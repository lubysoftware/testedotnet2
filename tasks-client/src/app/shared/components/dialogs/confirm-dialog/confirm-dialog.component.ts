import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent {
  title: string = 'Dialog';
  message: string = 'Message';
  buttonOk: string = '';
  buttonOkColor: string = '';
  buttonCancel: string = '';

  constructor(
    public readonly dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) { }

  confirm() {
    this.dialogRef.close(true);
  }
  cancel() {
    this.dialogRef.close(false);
  }
}
