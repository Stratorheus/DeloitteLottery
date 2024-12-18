import { Component } from '@angular/core';
import {GenerationStateService} from "../../core/services/generation-state.service";

@Component({
  selector: 'app-generation-toggle',
  standalone: true,
  imports: [],
  templateUrl: './generation-toggle.component.html',
  styleUrl: './generation-toggle.component.scss'
})
export class GenerationToggleComponent {
  public isServerSide: boolean | null = null;

  constructor(private stateService: GenerationStateService) {
    this.stateService.serverSideGeneration$.subscribe((value) => {
      this.isServerSide = value;
    });
  }

  toggleGenerationMode(): void {
    if (this.isServerSide !== null) {
      this.stateService.setGenerationMode(!this.isServerSide);
    }
    else{
      this.stateService.setGenerationMode(true);
    }
  }
}
