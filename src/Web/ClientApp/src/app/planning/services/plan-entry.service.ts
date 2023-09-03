import { Observable } from "rxjs";
import { Inject, Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { PlanEntryModel } from "./../model/plan-entry.model";

@Injectable()
export class PlanEntryService {
  readonly #httpClient: HttpClient;
  readonly #baseUrl: string;

  constructor(
    httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string)
  {
    this.#httpClient = httpClient;
    this.#baseUrl = baseUrl;
  }

  public findPlans(
    filter = '',
    sortOrder = 'asc',
    pageNumber = 0,
    pageSize = 3): Observable<PlanEntryModel[]>
  {
    const httpParams = new HttpParams()
      .set('filter', filter)
      .set('sortOrder', sortOrder)
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    const params = {
      params: httpParams
    };

    return this.#httpClient
      .get<PlanEntryModel[]>(this.#baseUrl + 'api/planentry', params);
  }
}
