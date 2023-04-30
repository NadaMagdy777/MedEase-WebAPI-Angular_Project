import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorPendingAppointmentsComponent } from './doctor-pending-appointments.component';

describe('DoctorPendingAppointmentsComponent', () => {
  let component: DoctorPendingAppointmentsComponent;
  let fixture: ComponentFixture<DoctorPendingAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorPendingAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorPendingAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
