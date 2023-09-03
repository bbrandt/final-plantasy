import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { PlanEntryService } from './plan-entry.service';
import { PlanEntryModel } from './../model/plan-entry.model';

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
      { eventDate: '2020-05-01', planType: 0, amount: 500 },
      { eventDate: '2012-12-12', planType: 1, amount: 7500 },
      { eventDate: '2001-01-07', planType: 0, amount: 1337 },
    ];

    const plans = planEntryService.findPlans();

    plans.subscribe((models) => {
      expect(models).toBe(testData);
    });

    const testRequest = httpTestingController
      .expectOne('TEST_BASE_URL/api/planentry?filter=&sortOrder=asc&pageNumber=0&pageSize=3');

    expect(testRequest.request.method).toBe('GET');

    testRequest.flush(testData);
  });
});
