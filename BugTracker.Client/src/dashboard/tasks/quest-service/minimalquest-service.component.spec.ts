import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinimalQuestServiceComponent } from './minimalquest-service.component';

describe('MinimalquestServiceComponentComponent', () => {
  let component: MinimalQuestServiceComponent;
  let fixture: ComponentFixture<MinimalQuestServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MinimalQuestServiceComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(MinimalQuestServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
