import { Component } from '@angular/core';
import {LotteryService} from "../../core/services/lottery.service";

@Component({
  selector: 'app-draw',
  standalone: true,
  imports: [],
  templateUrl: './draw.component.html',
  styleUrl: './draw.component.scss'
})
export class DrawComponent {
  public numbers: number[] = [];
  public error: string | null = null;

  constructor(private lotteryService: LotteryService) {}

  generate(): void {
    this.lotteryService.generate().subscribe({
      next: (numbers) => {
        this.numbers = numbers;
        this.error = null;
      },
      error: (err) => {
        console.error('Error generating numbers:', err);
        this.error = 'Failed to generate numbers.';
      },
    });
    console.log(this.numbers.join(","))
  }
}
