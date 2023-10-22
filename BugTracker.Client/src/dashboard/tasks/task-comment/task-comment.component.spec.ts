import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskCommentComponent } from './task-comment.component';

describe('TaskCommentComponent', () => {
  let component: TaskCommentComponent;
  let fixture: ComponentFixture<TaskCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskCommentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
