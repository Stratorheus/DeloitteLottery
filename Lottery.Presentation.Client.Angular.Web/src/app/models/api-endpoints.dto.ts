import {PagedResult} from "./paged-result.dto";
import {DrawLogDto} from "./draw-log.dto";
import {HistoryRequest} from "./history-request.dto";


export interface ApiSetGenerationModeRequest {
  isServerSide: boolean;
}

export interface ApiGetGenerationModeResponse {
  isServerSide: boolean;
}

export interface ApiGetHistoryRequest extends HistoryRequest {}

export interface ApiGetHistoryResponse extends PagedResult<DrawLogDto> {}
