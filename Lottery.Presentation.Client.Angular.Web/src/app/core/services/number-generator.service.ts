import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NumberGeneratorService {
  //IMPORTANT!!
  //Logics copied from backend C# code and rewritten to TS.
  //Please refer to Lottery > Application > Lottery.Application.Services project > NumberService.cs
  //to view comments and explanations

  private random = Math.random;

  private options = {
    min: 1,
    max: 50,
    count: 5,
    allowDuplicates: false,
  };

  generate(): number[] {
    return this.options.allowDuplicates ? this.generateWithDuplicates() : this.generateUnique();
  }

  private generateWithDuplicates(): number[] {
    const numbers = [];
    for (let i = 0; i < this.options.count; i++) {
      numbers.push(this.randomInRange(this.options.min, this.options.max));
    }
    return numbers;
  }

  private generateUnique(): number[] {
    const range = this.options.max - this.options.min + 1;
    return this.options.count <= range * 0.20
      ? this.generateUniqueWithHashSet()
      : this.generateUniqueWithFisherYates();
  }

  private generateUniqueWithFisherYates(): number[] {
    const numbers = Array.from(
      { length: this.options.max - this.options.min + 1 },
      (_, i) => i + this.options.min
    );

    for (let i = numbers.length - 1; i > 0; i--) {
      const j = Math.floor(this.random() * (i + 1));
      [numbers[i], numbers[j]] = [numbers[j], numbers[i]]; // Swap
    }

    return numbers.slice(0, this.options.count);
  }

  private generateUniqueWithHashSet(): number[] {
    const numbers = new Set<number>();
    while (numbers.size < this.options.count) {
      const number = this.randomInRange(this.options.min, this.options.max);
      numbers.add(number);
    }
    return Array.from(numbers);
  }

  private randomInRange(min: number, max: number): number {
    return Math.floor(this.random() * (max - min + 1)) + min;
  }
}
