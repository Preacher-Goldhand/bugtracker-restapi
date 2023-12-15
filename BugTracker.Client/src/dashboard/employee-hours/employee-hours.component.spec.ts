import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeHoursComponent } from './employee-hours.component';

describe('EmployeeHoursComponent', () => {
  let component: EmployeeHoursComponent;
  let fixture: ComponentFixture<EmployeeHoursComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeHoursComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeHoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
