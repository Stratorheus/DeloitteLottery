import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {GenerationToggleComponent} from "./components/generation-toggle/generation-toggle.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, GenerationToggleComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Lottery.Presentation.Client.Angular.Web';
}
