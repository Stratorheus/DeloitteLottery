import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from "rxjs";
import {ApiService} from "./api.service";

@Injectable({
  providedIn: 'root'
})
export class GenerationStateService {
  private serverSideGenerationSubject = new BehaviorSubject<boolean | null>(null);

  constructor(private apiService: ApiService) {}

  initialize(): void {
    this.apiService.getGenerationMode().subscribe({
      next: (response) => {
        this.serverSideGenerationSubject.next(response.isServerSide);
      },
      error: (err) => {
        console.error('Error fetching generation mode:', err);
        this.serverSideGenerationSubject.next(null);
      },
    });
  }

  get serverSideGeneration$(): Observable<boolean | null> {
    return this.serverSideGenerationSubject.asObservable();
  }

  setGenerationMode(isServerSide: boolean): void {
    this.apiService.setGenerationMode({isServerSide}).subscribe({
      next: (response) => {
        this.serverSideGenerationSubject.next(response.isServerSide);
      },
      error: (err) => {
        console.error('Error setting generation mode:', err);
      },
    });
  }
}
