import { Component, Type } from '@angular/core';
import { Subject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { PlanEntryEditorDialogService, PlanEntryEditorCommunicator } from './../services/plan-entry-editor-dialog.service';
import { PlanListComponentCommunicator } from './../plan-list/plan-list.component';
import { PlanEntryModel } from './../model/plan-entry.model';

@Component({
  selector: 'app-setup',
  styleUrls: ['./setup.component.css'],
  templateUrl: './setup.component.html'
})
export class SetupComponent {
  readonly #dialog: MatDialog;
  readonly #editorService: PlanEntryEditorDialogService;
  readonly #dialogEditorCommunicator: PlanEntryEditorCommunicator;

  public saveSubject: Subject<boolean>;
  public listCommunicator: PlanListComponentCommunicator;

  constructor(
    dialog: MatDialog,
    editorService: PlanEntryEditorDialogService)
  {
    this.#dialog = dialog;
    this.#editorService = editorService;

    this.saveSubject = new Subject<boolean>();

    this.listCommunicator = {
      deleteClick: (model: PlanEntryModel) => {
        this.deleteItem(model);
      },
      editClick: (model: PlanEntryModel) => {
        this.editItem(model);
      }
    };

    this.#dialogEditorCommunicator = {
      onExecuteAction: (action) => {
        this.onExecuteAction(action);
      }
    };
  }

  public addNewItem(): void {
    this.#editorService.add(this.#dialog, this.#dialogEditorCommunicator);
  }

  private deleteItem(model: PlanEntryModel): void {
    console.log(this);
  }

  private editItem(model: PlanEntryModel): void {
    this.#editorService.edit(this.#dialog, this.#dialogEditorCommunicator, model.id);
  }

  public onExecuteAction(action: Subject<boolean>): void {
    this.saveSubject = action;
  }
}
