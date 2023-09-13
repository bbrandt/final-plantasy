import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { PlanEntryService } from './plan-entry.service';
import { PlanEntryListModel } from './../model/plan-entry-list.model';
import { PersistentState } from './../model/persistent-state.enum';
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
    const testData: PlanEntryListModel[] = [
      { id: 1, eventDate: '2020-05-01', planTypeName: 'Credit', amount: 500, description: "First Line", repeatOnName: 'None', persistentState: PersistentState.None },
      { id: 2, eventDate: '2012-12-12', planTypeName: 'Debit', amount: 7500, description: "Second Line", repeatOnName: 'Bi-Weekly', persistentState: PersistentState.None },
      { id: 3, eventDate: '2001-01-07', planTypeName: 'Credit', amount: 1337, description: "Third Line", repeatOnName: 'Monthly', persistentState: PersistentState.None }
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
});
