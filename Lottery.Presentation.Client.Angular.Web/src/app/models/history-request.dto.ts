export interface HistoryRequest{
  pageIndex: number;
  pageSize: number;
  orderBy: string;
  descending: boolean;
  orderByNumberIndex?: number;
  fromDate?: string;
  toDate?: string;
  minNumber?: number;
  maxNumber?: number;
}
