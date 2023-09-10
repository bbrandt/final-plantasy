import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';

import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PlanEntryDeleteComponent } from './plan-entry-delete.component';

describe('PlanEntryDeleteComponent', () => {
  let component: PlanEntryDeleteComponent;
  let fixture: ComponentFixture<PlanEntryDeleteComponent>;
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [HttpClientTestingModule],
        declarations: [PlanEntryDeleteComponent],
        providers: [
          { provide: 'BASE_URL', useValue: 'TEST_BASE_URL/', deps: [] }
        ]
      })
      .compileComponents();

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanEntryDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
  });
});
