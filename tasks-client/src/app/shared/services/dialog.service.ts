import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs/internal/Observable';
import { ConfirmDialogComponent } from '../components/dialogs/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(
    private readonly dialog: MatDialog
  ) { }

  confirmRemove(message: string): Observable<boolean> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);
    dialogRef.componentInstance.title = 'Remover registro';
    dialogRef.componentInstance.message = message;
    dialogRef.componentInstance.buttonOk = 'REMOVER';
    dialogRef.componentInstance.buttonOkColor = 'warn';
    dialogRef.componentInstance.buttonCancel = 'CANCELAR';

    return dialogRef.afterClosed();
  }
}
