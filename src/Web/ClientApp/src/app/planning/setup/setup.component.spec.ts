import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { SetupComponent } from './setup.component';
import { AngularMaterialModule } from './../../material.module';

describe('SetupComponent', () => {
  let component: SetupComponent;
  let fixture: ComponentFixture<SetupComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [AngularMaterialModule, HttpClientTestingModule],
        declarations: [SetupComponent],
        providers: [
          { provide: 'BASE_URL', useValue: 'TEST_BASE_URL/', deps: [] }
        ]
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
