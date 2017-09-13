import { TestBed, inject } from '@angular/core/testing';

import { ComposeEmailFakeService } from './compose-email-fake.service';

describe('ComposeEmailFakeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ComposeEmailFakeService]
    });
  });

  it('should be created', inject([ComposeEmailFakeService], (service: ComposeEmailFakeService) => {
    expect(service).toBeTruthy();
  }));
});
