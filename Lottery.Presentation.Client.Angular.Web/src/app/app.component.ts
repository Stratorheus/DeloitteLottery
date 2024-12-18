import { Component } from '@angular/core';
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import {ApiService} from "./core/services/api.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  isApiAvailable: boolean = false;

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.checkApiStatus();
  }

  private checkApiStatus(): void {
    this.apiService.ping().subscribe({
      next: () => {
        this.isApiAvailable = true;
      },
      error: async (error) => {
        this.isApiAvailable = false;
      },
    });
  }
}
