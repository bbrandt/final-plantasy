import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BoundDialogComponent } from './bound-dialog.component';

describe('BoundDialogComponent', () => {
  let component: BoundDialogComponent;
  let fixture: ComponentFixture<BoundDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [BoundDialogComponent]
      })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoundDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
  });
});
