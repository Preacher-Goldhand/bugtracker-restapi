import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskImageComponent } from './task-image.component';

describe('TaskImageComponent', () => {
  let component: TaskImageComponent;
  let fixture: ComponentFixture<TaskImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
