import { Injectable } from '@angular/core';
import { Subject, take } from 'rxjs';
import { PlanEntryComponent } from './../plan-entry/plan-entry.component';
import { BoundDialogComponent } from '@shared/dialogs/bound-dialog.component';
import { BoundDialogData } from '@shared/dialogs/bound-dialog-data.interface';
import { MatDialog } from '@angular/material/dialog';
import { PlanEntryService } from './plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { PlanEntryDeleteComponent } from './../plan-entry-delete/plan-entry-delete.component';

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

  public delete(dialog: MatDialog, communicator: PlanEntryEditorCommunicator, model: PlanEntryModel): void {
    dialog.open<BoundDialogComponent, BoundDialogData>(BoundDialogComponent, {
      data: {
        boundComponent: PlanEntryDeleteComponent,
        title: 'Delete entry?',
        componentData: {
          model: model
        },
        actions: [
          {
            name: 'Cancel',
            callback: PlanEntryDeleteComponent.prototype.onCancelClick
          },
          {
            name: 'Delete',
            callback: PlanEntryDeleteComponent.prototype.onSaveClick,
            isDisabledCallback: PlanEntryDeleteComponent.prototype.isSaveDisabled,
            onExecute: communicator.onExecuteAction,
            color: 'warn'
          }
        ]
      }
    });
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
