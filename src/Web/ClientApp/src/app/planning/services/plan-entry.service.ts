import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PlanEntryModel } from './../model/plan-entry.model';
import { UrlBuilderService } from './../../services/url-builder.service';
import { ResultResponse } from './../model/result-response';
import { Response } from './../model/response';

@Injectable({
  providedIn: 'root'
})
export class PlanEntryService {
  readonly #httpClient: HttpClient;
  readonly #urlBuilder: UrlBuilderService;

  constructor(
    httpClient: HttpClient,
    urlBuilder: UrlBuilderService)
  {
    this.#httpClient = httpClient;
    this.#urlBuilder = urlBuilder;
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

    const url = this.#urlBuilder.build("api/plan-entry/list");

    return this.#httpClient
      .get<PlanEntryModel[]>(url, params);
  }

  public findPlan(id: number): Observable<PlanEntryModel> {
    const url = this.#urlBuilder.build(`api/plan-entry/${id}`);

    return this.#httpClient
      .get<PlanEntryModel>(url);
  }

  public updatePlan(model: PlanEntryModel): Observable<ResultResponse<number | null>> {
    const url = this.#urlBuilder.build('api/plan-entry/update');

    return this.#httpClient.put<ResultResponse<number | null>>(url, model);
  }

  public addPlan(model: PlanEntryModel): Observable<ResultResponse<number | null>> {
    const url = this.#urlBuilder.build('api/plan-entry/add');

    return this.#httpClient.post<ResultResponse<number | null>>(url, model);
  }

  public deletePlan(id: number): Observable<Response> {
    const url = this.#urlBuilder.build(`api/plan-entry/delete/${id}`);

    return this.#httpClient.delete<Response>(url);
  }
}
