import { Component } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DialogBodyDirective } from './dialog-body.directive';
import { BoundDialogComponent } from './bound-dialog.component';
import { AngularMaterialModule } from './../../material.module';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

describe('BoundDialogComponent', () => {
  let component: BoundDialogComponent;
  let fixture: ComponentFixture<BoundDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [AngularMaterialModule],
        declarations: [DialogBodyDirective, BoundDialogComponent],
        providers: [
          {
            provide: MAT_DIALOG_DATA, useValue: {
              boundComponent: TestComponentBody
            }
          },
          { provide: MatDialogRef, useValue: {} }
        ]
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


@Component({
  template: 'I am a test!',
  selector: 'test'
})
class TestComponentBody {
  constructor() {
  }
}
