import { Component } from '@angular/core';
import {DrawComponent} from "../../components/draw/draw.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    DrawComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
