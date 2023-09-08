import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SetupComponent } from './setup.component';
import { AngularMaterialModule } from './../../material.module';

describe('SetupComponent', () => {
  let component: SetupComponent;
  let fixture: ComponentFixture<SetupComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [AngularMaterialModule],
        declarations: [SetupComponent]
      })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
  });
});
