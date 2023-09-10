import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { PlanEntryService } from './plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';
import { PlanType } from './../model/plan-type.enum';
import { PlanRepeatOn } from './../model/plan-repeat-on.enum';
import { take } from 'rxjs';

describe("PlanEntryService", () => {
  let planEntryService: PlanEntryService;
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: 'BASE_URL', useValue: 'TEST_BASE_URL/', deps: [] }
      ]
    });

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
    planEntryService = TestBed.inject(PlanEntryService);
  });

  it('findPlans returns plans', () => {
    const testData: PlanEntryModel[] = [
      { eventDate: '2020-05-01', planType: PlanType.Credit, amount: 500, description: "First Line", repeatOn: PlanRepeatOn.None },
      { eventDate: '2012-12-12', planType: PlanType.Debit, amount: 7500, description: "Second Line", repeatOn: PlanRepeatOn.BiWeekly },
      { eventDate: '2001-01-07', planType: PlanType.Credit, amount: 1337, description: "Third Line", repeatOn: PlanRepeatOn.Monthly }
    ];

    const plans = planEntryService.findPlans();

    plans
      .pipe(take(1))
      .subscribe((models) => {
        expect(models).toBe(testData);
      });

    const testRequest = httpTestingController
      .expectOne('TEST_BASE_URL/api/plan-entry/list?filter=&sortOrder=asc&pageNumber=0&pageSize=3');

    expect(testRequest.request.method).toBe('GET');

    testRequest.flush(testData);
  });

  it('addOrUpdate plans returns a response', () => {

  });
});
