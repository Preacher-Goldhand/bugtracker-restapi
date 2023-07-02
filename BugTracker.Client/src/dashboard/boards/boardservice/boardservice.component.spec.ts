import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardServiceComponent } from './boardservice.component';

describe('BoardserviceComponent', () => {
  let component: BoardServiceComponent;
  let fixture: ComponentFixture<BoardServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BoardServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoardServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
