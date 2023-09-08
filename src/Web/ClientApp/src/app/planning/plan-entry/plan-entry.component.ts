import { Component } from '@angular/core';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PlanType } from './../model/plan-type.enum';
import { PlanTypeModel } from './../model/plan-type.model';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { ValidationMessage } from './../model/validation-message';
import { ResultResponse } from './../model/result-response';

@Component({
  selector: 'plan-entry',
  templateUrl: './plan-entry.component.html',
  styleUrls: ['./plan-entry.component.css']
})
export class PlanEntryComponent {
  #formBuilder: FormBuilder;
  #planEntryService: PlanEntryService;
  #isSaving: BehaviorSubject<boolean>;

  public planForm: FormGroup<FormType>;

  public planTypes: PlanTypeModel[];

  public isSaving$: Observable<boolean>;

  public validationMessages: ValidationMessage[];

  constructor(
    formBuilder: FormBuilder,
    planEntryService: PlanEntryService)
  {
    this.#formBuilder = formBuilder;
    this.#planEntryService = planEntryService;
    this.#isSaving = new BehaviorSubject<boolean>(false);
    this.isSaving$ = this.#isSaving.asObservable();
    this.validationMessages = [];

    this.planTypes = [
      { id: PlanType.Credit, name: "Credit" },
      { id: PlanType.Debit, name: "Debit" }
    ];

    this.planForm = this.createPlanForm();
  }

  public setEventDate(e: any) {
    const convertDate = new Date(e.target.value).toISOString().substring(0, 10);

    this.planForm.get('eventDate')?.setValue(convertDate, {
      onlyself: true,
    });
  }

  public onCancelClick(): Subject<boolean> {
    const subject = new BehaviorSubject<boolean>(true);

    // do async call

    return subject;
  }

  public onSaveClick(): Subject<boolean> {
    this.#isSaving.next(true);

    const subject = new Subject<boolean>();

    const model: PlanEntryModel = {
      amount: this.planForm.value.amount!,
      eventDate: this.planForm.value.eventDate!,
      planType: this.planForm.value.planType?.id!
    };

    this.#planEntryService.addOrUpdatePlan(model)
      .subscribe((response) => {
        this.validationMessages = response.messages;

        this.#isSaving.next(false);

        subject.next(false);
      });

    return subject;
  }

  public isSaveDisabled(): boolean {
    return !this.planForm.valid;
  }

  public hasValidations(): boolean {
    return this.validationMessages.length > 0;
  }

  private createPlanForm(): FormGroup {
    return this.#formBuilder.group({
      planType: [null, Validators.required],
      amount: [null, Validators.required],
      eventDate: [null, Validators.required]
    });
  }
}

interface FormType {
  planType: FormControl<PlanTypeModel | null>,
  amount: FormControl<number | null>,
  eventDate: FormControl<string | null>
}
