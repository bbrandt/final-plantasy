import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { OptionModel } from './../model/option.model';
import { PlanType } from './../model/plan-type.enum';
import { PlanRepeatOn } from './../model/plan-repeat-on.enum';

@Injectable({
  providedIn: 'root'
})
export class PlanOptionService {
  #planTypes: BehaviorSubject<OptionModel[]>;
  #repeatOns: BehaviorSubject<OptionModel[]>;

  constructor() {
    this.#planTypes = new BehaviorSubject<OptionModel[]>([
      { id: PlanType.Credit, name: 'Credit' },
      { id: PlanType.Debit, name: 'Debit' }
    ]);

    this.#repeatOns = new BehaviorSubject<OptionModel[]>([
      { id: PlanRepeatOn.None, name: 'None' },
      { id: PlanRepeatOn.BiWeekly, name: 'Bi-Weekly' },
      { id: PlanRepeatOn.Monthly, name: 'Monthly' },
      { id: PlanRepeatOn.Yearly, name: 'Yearly' }
    ]);
  }

  public getPlanTypes(): Observable<OptionModel[]> {
    return this.#planTypes.asObservable();
  }

  public getRepeatOns(): Observable<OptionModel[]> {
    return this.#repeatOns.asObservable();
  }
}
