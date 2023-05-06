import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientQuestionsComponent } from './patient-questions.component';

describe('PatientQuestionsComponent', () => {
  let component: PatientQuestionsComponent;
  let fixture: ComponentFixture<PatientQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientQuestionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
