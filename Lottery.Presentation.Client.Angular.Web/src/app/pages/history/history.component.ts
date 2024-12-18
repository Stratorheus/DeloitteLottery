import {Component, OnInit} from '@angular/core';
import {ApiService} from "../../core/services/api.service";
import {HistoryRequest} from "../../models/history-request.dto";
import {DrawLogDto} from "../../models/draw-log.dto";
import {DatePipe} from "@angular/common";
import {HistoryFilterComponent} from "../../components/history-filter/history-filter.component";
import {PagedResult} from "../../models/paged-result.dto";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [
    DatePipe,
    HistoryFilterComponent,
    FormsModule
  ],
  templateUrl: './history.component.html',
  styleUrl: './history.component.scss'
})
export class HistoryComponent implements OnInit {
  public history: PagedResult<DrawLogDto> = { items: [], totalCount: 0, pageIndex: 0, pageSize: 0 };
  public loading: boolean = false;

  //if we wanted to keep table read criteria even on redirect, we could utilize ng service (or even session storage in some cases)
  filterRequest : HistoryRequest = {
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

  pageSizeOptions: number[] = [10, 15, 20, 25, 50];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.fetchHistory();
  }

  onFilterSubmit(filter:HistoryRequest){
    this.filterRequest = { ...this.filterRequest, ...filter, pageIndex: 0 };
    this.fetchHistory();
  }

  onPageSizeChange(size: number): void {
    this.filterRequest.pageSize = Number(size);
    this.filterRequest.pageIndex = 0;
    this.fetchHistory();
  }

  goToPreviousPage(): void {
    if (this.filterRequest.pageIndex > 0) {
      this.filterRequest.pageIndex--;
      this.fetchHistory();
    }
  }

  goToNextPage(): void {
    if ((this.filterRequest.pageIndex + 1) * this.filterRequest.pageSize < this.history.totalCount) {
      this.filterRequest.pageIndex++;
      this.fetchHistory();
    }
  }

  setOrder(property: string, index? :number){
    if (this.filterRequest.orderBy !== property) {
      this.filterRequest.orderBy = property;
      this.filterRequest.descending = false;
      this.filterRequest.orderByNumberIndex = property === 'NumberIndex' ? index : undefined;
    }
    else {
      if (property === 'NumberIndex' && this.filterRequest.orderByNumberIndex !== index) {
        this.filterRequest.orderByNumberIndex = index;
      } else if (this.filterRequest.descending) {
        this.filterRequest.orderBy = '';
        this.filterRequest.orderByNumberIndex = undefined;
      } else {
        this.filterRequest.descending = true;
      }
    }

    this.fetchHistory();
  }

  fetchHistory(): void {
    this.loading = true;
    this.apiService.getHistory(this.filterRequest).subscribe({
      next: (data: any) => {
        this.history = data;
      },
      error: (err) => {
        console.error('Error fetching draw history:', err);
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  protected readonly Math = Math;
}
