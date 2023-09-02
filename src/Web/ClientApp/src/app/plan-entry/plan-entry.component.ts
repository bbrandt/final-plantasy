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

  public hasRepeatOnDate(): boolean {
    return this.planForm.value.planType?.id == 1;
  }

  public onSubmit(): void {
    console.log(this.planForm.value);
  }

  public setRepeatOnDate(e: any) {
    const convertDate = new Date(e.target.value).toISOString().substring(0, 10);

    this.planForm.get('repeatOnDate')?.setValue(convertDate, {
      onlyself: true,
    });
  }

  private createPlanForm(): FormGroup {
    return this.#formBuilder.group({
      planType: [null, Validators.required],
      amount: [null, Validators.required],
      repeatOnDate: [null]
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
  repeatOnDate: FormControl<string | null>
}
