import { Component, OnInit } from '@angular/core';
import { Subject, BehaviorSubject, take } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { OptionModel } from './../model/option.model';
import { PlanEntryService } from './../services/plan-entry.service';
import { PlanOptionService } from './../services/plan-option.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { ValidationMessage } from './../model/validation-message';
import { PlanRepeatOn } from '../model/plan-repeat-on.enum';

@Component({
  selector: 'plan-entry',
  templateUrl: './plan-entry.component.html',
  styleUrls: ['./plan-entry.component.css']
})
export class PlanEntryComponent implements OnInit {
  readonly #formBuilder: FormBuilder;
  readonly #planEntryService: PlanEntryService;
  readonly #planOptionService: PlanOptionService;

  public planForm: FormGroup<FormType>;

  public planTypes!: OptionModel[];

  public repeatOptions!: OptionModel[];

  public validationMessages: ValidationMessage[];

  constructor(
    formBuilder: FormBuilder,
    planEntryService: PlanEntryService,
    planOptionService: PlanOptionService)
  {
    this.#formBuilder = formBuilder;
    this.#planEntryService = planEntryService;
    this.#planOptionService = planOptionService;
    this.validationMessages = [];

    this.planForm = this.createPlanForm();
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

    this.#planEntryService.addOrUpdatePlan(model)
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
