import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PlanEntryComponent } from './plan-entry.component';

describe('PlanEntryComponent', () => {
  let component: PlanEntryComponent;
  let fixture: ComponentFixture<PlanEntryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [PlanEntryComponent]
      })
      .compileComponents();
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
