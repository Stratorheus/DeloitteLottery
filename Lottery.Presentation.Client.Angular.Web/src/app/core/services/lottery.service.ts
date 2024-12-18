import { Injectable } from '@angular/core';
import {ApiService} from "./api.service";
import {NumberGeneratorService} from "./number-generator.service";
import {GenerationStateService} from "./generation-state.service";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class LotteryService {
  public isServerSide: boolean | null = null;
  constructor(
    private apiService: ApiService,
    private numberGeneratorService: NumberGeneratorService,
    private stateService: GenerationStateService
  ) {this.stateService.serverSideGeneration$.subscribe((value) => {
    this.isServerSide = value;
  });}

  generate(): Observable<number[]> {
    if (this.isServerSide === true) {
      // Server-side generating
      return this.apiService.generateNumbers();
    } else if (this.isServerSide === false) {
      // Client-side generating
      const numbers = this.numberGeneratorService.generate();
      return this.apiService.saveDraw(numbers).pipe(map(() => numbers));
    } else {
      throw new Error('Generation mode is not defined.');
    }
  }
}
