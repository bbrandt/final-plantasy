import { Component, OnInit } from '@angular/core';
import { Subject, BehaviorSubject, take } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DateTime } from 'luxon';
import { OptionModel } from './../model/option.model';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanOptionService } from './../services/plan-option.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { ValidationMessage } from './../model/validation-message';
import { PersistentState } from './../model/persistent-state.enum';

@Component({
  selector: 'plan-entry',
  templateUrl: './plan-entry.component.html',
  styleUrls: ['./plan-entry.component.css']
})
export class PlanEntryComponent implements OnInit {
  readonly #formBuilder: FormBuilder;
  readonly #planEntryService: PlanEntryService;
  readonly #planOptionService: PlanOptionService;

  public planForm!: FormGroup<FormType>;
  public planTypes!: OptionModel[];
  public repeatOptions!: OptionModel[];
  public validationMessages: ValidationMessage[];
  public componentData!: PlanEntryComponentData;

  constructor(
    formBuilder: FormBuilder,
    planEntryService: PlanEntryService,
    planOptionService: PlanOptionService)
  {
    this.#formBuilder = formBuilder;
    this.#planEntryService = planEntryService;
    this.#planOptionService = planOptionService;
    this.validationMessages = [];
  }

  ngOnInit(): void {
    this.#planOptionService.getPlanTypes()
      .pipe(take(1))
      .subscribe((options) => {
        this.planTypes = options;
      });

    this.#planOptionService.getRepeatOns()
      .pipe(take(1))
      .subscribe((options) => {
        this.repeatOptions = options;
      });

    this.planForm = this.createPlanForm();
  }

  public onCancelClick(): Subject<boolean> {
    const subject = new BehaviorSubject<boolean>(true);

    return subject;
  }

  public onSaveClick(): Subject<boolean> {
    const subject = new Subject<boolean>();

    const model: PlanEntryModel = {
      id: this.componentData?.model?.id,
      amount: this.planForm.value.amount!,
      eventDate: this.planForm.value.eventDate!.toISODate()!,
      planType: this.planForm.value.planType?.id!,
      description: this.planForm.value.description!,
      repeatOn: this.planForm.value.repeatOn?.id!,
      endDate: this.planForm.value.endDate?.toISODate(),
      persistentState: this.componentData?.model?.id ?
        PersistentState.Updated :
        PersistentState.Added
    };

    const method = model.persistentState == PersistentState.Updated ?
      this.#planEntryService.updatePlan :
      this.#planEntryService.addPlan;

    method.call(this.#planEntryService, model)
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
    return !this.planForm.valid;
  }

  public hasValidations(): boolean {
    return this.validationMessages.length > 0;
  }

  private createPlanForm(): FormGroup {
    const model = this.componentData?.model;

    const modelPlanType = this.findOption(this.planTypes, model?.planType);
    const modelRepeatOn = this.findOption(this.repeatOptions, model?.repeatOn);

    return this.#formBuilder.group({
      planType: [modelPlanType, Validators.required],
      amount: [model?.amount, Validators.required],
      eventDate: [model?.eventDate, Validators.required],
      description: [model?.description, Validators.required],
      repeatOn: [modelRepeatOn, Validators.required],
      endDate: [model?.endDate]
    });
  }

  private findOption(options: OptionModel[], id: number | undefined): OptionModel | null | undefined {
    if (id === undefined) {
      return null;
    }

    const found = options.find(
      (option) => {
        return option.id === id;
      });

    return found;
  }
}

interface FormType {
  planType: FormControl<OptionModel | null>,
  amount: FormControl<number | null>,
  eventDate: FormControl<DateTime | null>,
  description: FormControl<string | null>,
  repeatOn: FormControl<OptionModel | null>,
  endDate: FormControl<DateTime | null>
}

export interface PlanEntryComponentData {
  model?: PlanEntryModel | null;
}
