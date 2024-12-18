import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {
  ApiGenerateResponse, ApiGetHistoryRequest,
  ApiGetHistoryResponse,
  ApiGetOrderableFieldsResponse, ApiSaveDrawRequest,
  ApiSaveDrawResponse, ApiSetGenerationModeRequest
} from "../../models/api-endpoints.dto";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl: string = 'https://localhost:7220/api/draw';

  constructor(private http: HttpClient) {}

  generateNumbers(): Observable<ApiGenerateResponse> {
    return this.http.get<ApiGenerateResponse>(`${this.baseUrl}/generate`);
  }

  setGenerationMode(request: ApiSetGenerationModeRequest): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.baseUrl}/set-generation-mode`, request);
  }

  saveDraw(request: ApiSaveDrawRequest): Observable<ApiSaveDrawResponse> {
    return this.http.post<ApiSaveDrawResponse>(`${this.baseUrl}/save`, request);
  }

  getHistory(request: ApiGetHistoryRequest): Observable<ApiGetHistoryResponse> {
    return this.http.post<ApiGetHistoryResponse>(`${this.baseUrl}/history`, request);
  }

  getOrderableFields(): Observable<ApiGetOrderableFieldsResponse> {
    return this.http.get<ApiGetOrderableFieldsResponse>(`${this.baseUrl}/orderable-fields`);
  }
}
