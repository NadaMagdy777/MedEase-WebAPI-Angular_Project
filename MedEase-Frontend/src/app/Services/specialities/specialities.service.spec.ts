import { TestBed } from '@angular/core/testing';

import { SpecialtiesService } from './specialities.service';

describe('SpecialitiesService', () => {
  let service: SpecialtiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SpecialtiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
