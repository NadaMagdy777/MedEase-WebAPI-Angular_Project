import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReserveAppointementComponent } from './reserve-appointement.component';

describe('ReserveAppointementComponent', () => {
  let component: ReserveAppointementComponent;
  let fixture: ComponentFixture<ReserveAppointementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReserveAppointementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReserveAppointementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
