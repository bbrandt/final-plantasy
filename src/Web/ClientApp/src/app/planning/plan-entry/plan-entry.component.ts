import { Component } from '@angular/core';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PlanType } from './../model/plan-type.enum';
import { OptionModel } from './../model/option.model';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { ValidationMessage } from './../model/validation-message';
import { PlanRepeatOn } from '../model/plan-repeat-on.enum';

@Component({
  selector: 'plan-entry',
  templateUrl: './plan-entry.component.html',
  styleUrls: ['./plan-entry.component.css']
})
export class PlanEntryComponent {
  #formBuilder: FormBuilder;
  #planEntryService: PlanEntryService;

  public planForm: FormGroup<FormType>;

  public planTypes: OptionModel[];

  public repeatOptions: OptionModel[];

  public validationMessages: ValidationMessage[];

  constructor(
    formBuilder: FormBuilder,
    planEntryService: PlanEntryService)
  {
    this.#formBuilder = formBuilder;
    this.#planEntryService = planEntryService;
    this.validationMessages = [];

    this.planTypes = [
      { id: PlanType.Credit, name: 'Credit' },
      { id: PlanType.Debit, name: 'Debit' }
    ];

    this.repeatOptions = [
      { id: PlanRepeatOn.None, name: 'None' },
      { id: PlanRepeatOn.BiWeekly, name: 'Bi-Weekly' },
      { id: PlanRepeatOn.Monthly, name: 'Monthly' },
      { id: PlanRepeatOn.Yearly, name: 'Yearly' }
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

    return subject;
  }

  public onSaveClick(): Subject<boolean> {
    const subject = new Subject<boolean>();

    const model: PlanEntryModel = {
      amount: this.planForm.value.amount!,
      eventDate: this.planForm.value.eventDate!,
      planType: this.planForm.value.planType?.id!,
      description: this.planForm.value.description!,
      repeatOn: this.planForm.value.repeatOn?.id!
    };

    const addOrUpdate = this.#planEntryService.addOrUpdatePlan(model)
      .subscribe((response) => {
        this.validationMessages = response.messages;

        subject.next(true);

        addOrUpdate.unsubscribe();
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
      eventDate: [null, Validators.required],
      description: [null, Validators.required],
      repeatOn: [PlanRepeatOn.None, Validators.required]
    });
  }
}

interface FormType {
  planType: FormControl<OptionModel | null>,
  amount: FormControl<number | null>,
  eventDate: FormControl<string | null>,
  description: FormControl<string | null>,
  repeatOn: FormControl<OptionModel | null>
}
