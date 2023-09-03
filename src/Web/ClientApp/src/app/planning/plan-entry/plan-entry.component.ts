import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'plan-entry',
  templateUrl: './plan-entry.component.html',
  styleUrls: ['./plan-entry.component.css']
})
export class PlanEntryComponent {
  #formBuilder: FormBuilder;

  public planForm: FormGroup<FormType>;

  public planTypes: PlanType[];

  public hide: boolean = true;

  constructor(formBuilder: FormBuilder) {
    this.#formBuilder = formBuilder;
    this.planTypes = [
      { id: 0, name: "Credit" },
      { id: 1, name: "Debit" }
    ];

    this.planForm = this.createPlanForm();
  }

  public onSubmit(): void {
    console.log(this.planForm.value);
  }

  public setEventDate(e: any) {
    const convertDate = new Date(e.target.value).toISOString().substring(0, 10);

    this.planForm.get('eventDate')?.setValue(convertDate, {
      onlyself: true,
    });
  }

  public onCancelClick(): boolean {
    return true;
  }

  public onSaveClick(): boolean {
    return true;
  }

  public isSaveDisabled(): boolean {
    return !this.planForm.valid;
  }

  private createPlanForm(): FormGroup {
    return this.#formBuilder.group({
      planType: [null, Validators.required],
      amount: [null, Validators.required],
      eventDate: [null, Validators.required]
    });
  }
}

interface PlanType {
  id: number,
  name: string
}

interface FormType {
  planType: FormControl<PlanType | null>,
  amount: FormControl<number | null>,
  eventDate: FormControl<string | null>
}
