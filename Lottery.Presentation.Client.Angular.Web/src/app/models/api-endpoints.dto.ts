import {PagedResult} from "./paged-result.dto";
import {DrawLogDto} from "./draw-log.dto";
import {HistoryRequest} from "./history-request.dto";

export interface ApiGenerateResponse {
  numbers: number[];
}

export interface ApiSetGenerationModeRequest {
  isServerSide: boolean;
}

export interface ApiGetGenerationModeResponse {
  isServerSide: boolean;
}

export interface ApiSaveDrawRequest {
  numbers: number[];
}

export interface ApiSaveDrawResponse {
  message: string;
}

export interface ApiGetHistoryRequest extends HistoryRequest {}

export interface ApiGetHistoryResponse extends PagedResult<DrawLogDto> {}

export interface ApiGetOrderableFieldsResponse {
  fields: string[];
}
