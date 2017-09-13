import { TestBed, inject } from '@angular/core/testing';

import { ComposeEmailService } from './compose-email.service';

describe('ComposeEmailService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ComposeEmailService]
    });
  });

  it('should be created', inject([ComposeEmailService], (service: ComposeEmailService) => {
    expect(service).toBeTruthy();
  }));
});
