export interface DrawLogDto {
  id: number;
  created: string;
  numbers: DrawNumberDto[];
}

export interface DrawNumberDto {
  number: number;
  index: number;
}
