import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';

import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PlanEntryComponent } from './plan-entry.component';

describe('PlanEntryComponent', () => {
  let component: PlanEntryComponent;
  let fixture: ComponentFixture<PlanEntryComponent>;
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [HttpClientTestingModule],
        declarations: [PlanEntryComponent],
        providers: [
          { provide: 'BASE_URL', useValue: 'TEST_BASE_URL/', deps: [] }
        ]
      })
      .compileComponents();

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
  });
});
