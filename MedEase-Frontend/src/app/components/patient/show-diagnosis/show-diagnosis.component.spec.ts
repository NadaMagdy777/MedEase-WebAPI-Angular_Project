import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowDiagnosisComponent } from './show-diagnosis.component';

describe('ShowDiagnosisComponent', () => {
  let component: ShowDiagnosisComponent;
  let fixture: ComponentFixture<ShowDiagnosisComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowDiagnosisComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowDiagnosisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
