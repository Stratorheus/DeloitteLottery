import { TestBed } from '@angular/core/testing';

import { GenerationStateService } from './generation-state.service';

describe('GenerationStateService', () => {
  let service: GenerationStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenerationStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
