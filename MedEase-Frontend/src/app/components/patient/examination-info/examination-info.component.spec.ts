import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationInfoComponent } from './examination-info.component';

describe('ExaminationInfoComponent', () => {
  let component: ExaminationInfoComponent;
  let fixture: ComponentFixture<ExaminationInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExaminationInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExaminationInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
