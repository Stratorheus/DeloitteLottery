import {Component, OnInit} from '@angular/core';
import {ApiService} from "../../core/services/api.service";
import {HistoryRequest} from "../../models/history-request.dto";

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [],
  templateUrl: './history.component.html',
  styleUrl: './history.component.scss'
})
export class HistoryComponent implements OnInit {
  public history: any[] = [];
  public loading: boolean = false;

  private filterRequest : HistoryRequest = {
    pageIndex: 0,
    pageSize: 10,
    orderBy: 'Created',
    descending: true,
    orderByNumberIndex: undefined,
    fromDate: undefined,
    toDate: undefined,
    minNumber: undefined,
    maxNumber: undefined,
  };

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.fetchHistory();
  }

  fetchHistory(): void {
    this.loading = true;
    this.apiService.getHistory(this.filterRequest).subscribe({
      next: (data: any) => {
        this.history = data.items;
      },
      error: (err) => {
        console.error('Error fetching draw history:', err);
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
}
