import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionsLayoutComponent } from './questions-layout.component';

describe('QuestionsLayoutComponent', () => {
  let component: QuestionsLayoutComponent;
  let fixture: ComponentFixture<QuestionsLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionsLayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestionsLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
