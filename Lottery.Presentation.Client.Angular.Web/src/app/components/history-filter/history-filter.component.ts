import {Component, EventEmitter, Output, signal, WritableSignal} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {HistoryRequest} from "../../models/history-request.dto";

@Component({
  selector: 'app-history-filter',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './history-filter.component.html',
  styleUrl: './history-filter.component.scss'
})
export class HistoryFilterComponent {
  filterForm: FormGroup;

  //Personally I'd rather use signals, however currently I'm sadly not knowledgeable enough about signals <> reactive forms integration
  @Output() filterSubmitted = new EventEmitter<HistoryRequest>();

  constructor(private fb: FormBuilder) {
    this.filterForm = this.fb.group({
      fromDate: [null],
      toDate: [null],
      minNumber: [null],
      maxNumber: [null],
    });
  }

  submitFilter(): void {
    this.filterSubmitted.emit(this.filterForm.value);
  }
}
