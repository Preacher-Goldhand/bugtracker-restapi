import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeServiceComponent } from './employee-service.component';

describe('EmployeeServiceComponent', () => {
  let component: EmployeeServiceComponent;
  let fixture: ComponentFixture<EmployeeServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
