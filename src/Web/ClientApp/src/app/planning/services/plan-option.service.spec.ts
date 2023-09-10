import { TestBed } from '@angular/core/testing';
import { PlanOptionService } from './plan-option.service';
import { OptionModel } from './../model/option.model';
import { PlanType } from './../model/plan-type.enum';
import { PlanRepeatOn } from './../model/plan-repeat-on.enum';
import { take } from 'rxjs';

describe("PlanOptionService", () => {
  let planOptionService: PlanOptionService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    });

    planOptionService = TestBed.inject(PlanOptionService);
  });

  it('getPlanTypes returns plan types', () => {
    const testData: OptionModel[] = [
      { id: PlanType.Credit, name: 'Credit' },
      { id: PlanType.Debit, name: 'Debit' }
    ];

    const options = planOptionService.getPlanTypes();

    options
      .pipe(take(1))
      .subscribe((models) => {
        expect(models).toEqual(testData);
      });
  });

  it('getRepeatOns returns repeat ons', () => {
    const testData: OptionModel[] = [
      { id: PlanRepeatOn.None, name: 'None' },
      { id: PlanRepeatOn.BiWeekly, name: 'Bi-Weekly' },
      { id: PlanRepeatOn.Monthly, name: 'Monthly' },
      { id: PlanRepeatOn.Yearly, name: 'Yearly' }
    ];

    const options = planOptionService.getRepeatOns();

    options
      .pipe(take(1))
      .subscribe((models) => {
        expect(models).toEqual(testData);
      });
  });
});
