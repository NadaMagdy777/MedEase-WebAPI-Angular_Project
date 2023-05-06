import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAnswerdQuestionsComponent } from './doctor-answerd-questions.component';

describe('DoctorAnswerdQuestionsComponent', () => {
  let component: DoctorAnswerdQuestionsComponent;
  let fixture: ComponentFixture<DoctorAnswerdQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorAnswerdQuestionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorAnswerdQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
