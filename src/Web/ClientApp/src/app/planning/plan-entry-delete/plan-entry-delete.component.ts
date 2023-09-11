import { Component, OnInit } from '@angular/core';
import { Subject, BehaviorSubject, take } from 'rxjs';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { ValidationMessage } from './../model/validation-message';

@Component({
  selector: 'plan-entry-delete',
  templateUrl: './plan-entry-delete.component.html',
  styleUrls: ['./plan-entry-delete.component.css']
})
export class PlanEntryDeleteComponent implements OnInit {
  readonly #planEntryService: PlanEntryService;

  public validationMessages: ValidationMessage[];
  public componentData!: PlanDeleteComponentData;
  public description?: string;

  constructor(
    planEntryService: PlanEntryService)
  {
    this.#planEntryService = planEntryService;
    this.validationMessages = [];
  }

  ngOnInit(): void {
    this.description = this.componentData?.model?.description;
  }

  public onCancelClick(): Subject<boolean> {
    const subject = new BehaviorSubject<boolean>(true);

    return subject;
  }

  public onSaveClick(): Subject<boolean> {
    const subject = new Subject<boolean>();

    const model = this.componentData?.model;

    if (!model || !model.id) {
      throw new Error("Id is undefined");
    }

    this.#planEntryService.deletePlan(model.id!)
      .pipe(take(1))
      .subscribe((response) => {
        this.validationMessages = response.messages;

        if (this.validationMessages.length > 0) {
          subject.next(false);
          return;
        }

        subject.next(true);
      });

    return subject;
  }

  public isSaveDisabled(): boolean {
    return false;
  }

  public hasValidations(): boolean {
    return this.validationMessages.length > 0;
  }
}

export interface PlanDeleteComponentData {
  model?: PlanEntryModel | null;
}
