import { Component } from '@angular/core';
import {DrawComponent} from "../../components/draw/draw.component";
import {GenerationToggleComponent} from "../../components/generation-toggle/generation-toggle.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    DrawComponent,
    GenerationToggleComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
