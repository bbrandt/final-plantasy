import { Component, ComponentRef, OnInit, OnDestroy, ViewChild, Inject } from '@angular/core';
import { DialogBodyDirective } from './dialog-body.directive';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BoundDialogData } from './bound-dialog-data.interface';
import { BoundDialogAction } from './bound-dialog-action.interface';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'bound-dialog',
  styleUrls: ['./bound-dialog.component.css'],
  templateUrl: './bound-dialog.component.html'
})
export class BoundDialogComponent implements OnInit, OnDestroy
{
  @ViewChild(DialogBodyDirective, { static: true })
  public dialogBody!: DialogBodyDirective;
  public isBusy$: Observable<boolean>;

  readonly #data: BoundDialogData;
  readonly #dialogRef: MatDialogRef<BoundDialogComponent>
  readonly #isBusy: BehaviorSubject<boolean>;
  #componentRef!: ComponentRef<any>;

  constructor(
    @Inject(MAT_DIALOG_DATA) data: BoundDialogData,
    public dialogRef: MatDialogRef<BoundDialogComponent>)
  {
    this.#data = data;
    this.#dialogRef = dialogRef;
    this.#isBusy = new BehaviorSubject<boolean>(false);
    this.isBusy$ = this.#isBusy.asObservable();
  }

  public ngOnInit(): void {
    this.#componentRef = this.dialogBody.viewContainerRef.createComponent(this.#data.boundComponent);
  }

  public ngOnDestroy(): void {
      
  }

  public title(): string {
    return this.#data.title ?? '';
  }

  public hasActions(): boolean {
    return this.#data.actions && this.#data.actions.length > 0;
  }

  public getActions(): Array<any> {
    return this.#data.actions;
  }

  public executeAction(action: BoundDialogAction): void {
    this.#isBusy.next(true);

    const closeSubject = action.callback.call(this.#componentRef.instance);

    if (action.onExecute) {
      action.onExecute(closeSubject);
    }

    closeSubject.subscribe((canClose: boolean) => {
      this.#isBusy.next(false);

      if (canClose) {
        this.#isBusy.complete();
        this.#dialogRef.close();

        closeSubject.unsubscribe();
      }
    });
  }

  public isActionDisabled(action: BoundDialogAction): boolean {
    if (this.#isBusy.getValue()) {
      return true;
    }

    if (!action.isDisabledCallback) {
      return false;
    }

    const isDisabled = action.isDisabledCallback.call(this.#componentRef.instance);

    return isDisabled;
  }

  public getActionColor(action: BoundDialogAction): string {
    return action.color ? action.color : '';
  }
}
