import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmedAppointmentComponent } from './confirmed-appointment.component';

describe('ConfirmedAppointmentComponent', () => {
  let component: ConfirmedAppointmentComponent;
  let fixture: ComponentFixture<ConfirmedAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmedAppointmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmedAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
