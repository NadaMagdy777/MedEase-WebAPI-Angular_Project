import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorUnAnswerdComponent } from './doctor-un-answerd.component';

describe('DoctorUnAnswerdComponent', () => {
  let component: DoctorUnAnswerdComponent;
  let fixture: ComponentFixture<DoctorUnAnswerdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorUnAnswerdComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorUnAnswerdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
