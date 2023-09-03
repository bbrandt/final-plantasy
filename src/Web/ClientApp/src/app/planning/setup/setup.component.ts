import { Component, Type } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PlanEntryComponent } from './../plan-entry/plan-entry.component';
import { BoundDialogComponent } from './../dialogs/bound-dialog.component';
import { BoundDialogData } from './../dialogs/bound-dialog-data.interface';

@Component({
  selector: 'app-setup',
  styleUrls: ['./setup.component.css'],
  templateUrl: './setup.component.html'
})
export class SetupComponent {
  #dialog: MatDialog;

  constructor(dialog: MatDialog) {
    this.#dialog = dialog;
  }

  public addNewItem(): void {
    this.#dialog.open<BoundDialogComponent, BoundDialogData>(BoundDialogComponent, {
      data: {
        boundComponent: PlanEntryComponent,
        title: "Add New Entry",
        actions: [
          {
            name: "Cancel",
            callback: PlanEntryComponent.prototype.onCancelClick
          },
          {
            name: "Save",
            callback: PlanEntryComponent.prototype.onSaveClick,
            isDisabledCallback: PlanEntryComponent.prototype.isSaveDisabled
          }
        ]
      }
    });
  }
}
