import { Injectable } from '@angular/core';
import { Subject, take } from 'rxjs';
import { PlanEntryComponent } from './../plan-entry/plan-entry.component';
import { BoundDialogComponent } from './../dialogs/bound-dialog.component';
import { BoundDialogData } from './../dialogs/bound-dialog-data.interface';
import { MatDialog } from '@angular/material/dialog';
import { PlanEntryService } from './plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';

@Injectable({
  providedIn: 'root'
})
export class PlanEntryEditorDialogService {
  readonly #planEntryService: PlanEntryService;

  constructor(planEntryService: PlanEntryService) {
    this.#planEntryService = planEntryService;
  }

  public add(dialog: MatDialog, communicator: PlanEntryEditorCommunicator): void {
    this.openDialog(dialog, communicator, 'Add New Entry');
  }

  public edit(dialog: MatDialog, communicator: PlanEntryEditorCommunicator, id: number | undefined): void {
    if (!id) {
      throw new Error('id is undefined');
    }

    this.#planEntryService
      .findPlan(id!)
      .pipe(take(1))
      .subscribe((model) => {
        this.openDialog(dialog, communicator, 'Edit Entry', model);
      });
  }

  public delete(dialog: MatDialog, communicator: PlanEntryEditorCommunicator, id: number, description: string): void {
    // prompt for delete
  }

  private openDialog(
    dialog: MatDialog,
    communicator: PlanEntryEditorCommunicator,
    title: string,
    model: PlanEntryModel | null = null): void
  {
    dialog.open<BoundDialogComponent, BoundDialogData>(BoundDialogComponent, {
      data: {
        boundComponent: PlanEntryComponent,
        title: title,
        componentData: {
          model: model
        },
        actions: [
          {
            name: 'Cancel',
            callback: PlanEntryComponent.prototype.onCancelClick
          },
          {
            name: 'Save',
            callback: PlanEntryComponent.prototype.onSaveClick,
            isDisabledCallback: PlanEntryComponent.prototype.isSaveDisabled,
            onExecute: communicator.onExecuteAction,
            color: 'primary'
          }
        ]
      }
    });
  }
}

export interface PlanEntryEditorCommunicator {
  onExecuteAction: (action: Subject<boolean>) => void;
}
