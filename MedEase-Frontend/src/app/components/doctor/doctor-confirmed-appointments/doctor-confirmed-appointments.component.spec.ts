import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorConfirmedAppointmentsComponent } from './doctor-confirmed-appointments.component';

describe('DoctorConfirmedAppointmentsComponent', () => {
  let component: DoctorConfirmedAppointmentsComponent;
  let fixture: ComponentFixture<DoctorConfirmedAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorConfirmedAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorConfirmedAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
