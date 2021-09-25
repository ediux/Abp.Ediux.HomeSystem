import { TestBed } from '@angular/core/testing';

import { SmartAdminUIService } from './smart-admin-uI.service';

describe('SmartAdminUIService', () => {
  let service: SmartAdminUIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmartAdminUIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
