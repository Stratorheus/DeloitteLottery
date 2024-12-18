import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {
  ApiGenerateResponse, ApiGetGenerationModeResponse, ApiGetHistoryRequest,
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

  ping(): Observable<void> {
    return this.http.get<void>(`${this.baseUrl}/ping`);
  }

  generateNumbers(): Observable<number[]> {
    return this.http.get<number[]>(`${this.baseUrl}/generate`);
  }

  setGenerationMode(request: ApiSetGenerationModeRequest) : Observable<{ isServerSide: boolean; message: string }> {
    return this.http.post<{ isServerSide: boolean; message: string }>(`${this.baseUrl}/generation-mode`, request.isServerSide);
  }

  getGenerationMode() : Observable<ApiGetGenerationModeResponse> {
    return this.http.get<ApiGetGenerationModeResponse>(`${this.baseUrl}/generation-mode`);
  }

  saveDraw(request: number[]): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/save`, request);
  }

  getHistory(request: ApiGetHistoryRequest): Observable<ApiGetHistoryResponse> {
    return this.http.post<ApiGetHistoryResponse>(`${this.baseUrl}/history`, request);
  }

  getOrderableFields(): Observable<ApiGetOrderableFieldsResponse> {
    return this.http.get<ApiGetOrderableFieldsResponse>(`${this.baseUrl}/orderable-fields`);
  }
}
