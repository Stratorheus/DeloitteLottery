import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerationToggleComponent } from './generation-toggle.component';

describe('GenerationToggleComponent', () => {
  let component: GenerationToggleComponent;
  let fixture: ComponentFixture<GenerationToggleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenerationToggleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GenerationToggleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
