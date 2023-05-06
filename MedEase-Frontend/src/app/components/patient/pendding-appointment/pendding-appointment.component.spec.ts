import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PenddingAppointmentComponent } from './pendding-appointment.component';

describe('PenddingAppointmentComponent', () => {
  let component: PenddingAppointmentComponent;
  let fixture: ComponentFixture<PenddingAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PenddingAppointmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PenddingAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
