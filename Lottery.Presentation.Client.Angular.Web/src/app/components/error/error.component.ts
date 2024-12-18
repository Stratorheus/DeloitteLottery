import { Component } from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {firstValueFrom} from "rxjs";
import {ApiService} from "../../core/services/api.service";

@Component({
  selector: 'app-error',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './error.component.html',
  styleUrl: './error.component.scss'
})
export class ErrorComponent {
  public checkingApi: boolean = false;
  public errorMessage: string = 'The backend API is currently unavailable.';

  constructor(private apiService: ApiService, private router: Router) {}

  retry(): void {
    this.checkingApi = true;

    firstValueFrom(this.apiService.ping())
      .then( async () => {
        await this.router.navigate(['/']);
      })
      .catch((error) => {
        console.error('API still unavailable:', error);
        this.errorMessage = 'The backend API is still unavailable. Please try again later.';
      })
      .finally(() => {
        this.checkingApi = false;
      });
  }
}
