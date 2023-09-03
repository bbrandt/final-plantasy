import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { PlanEntryModel } from "./../model/plan-entry.model";
import { UrlBuilderService } from "./../../services/url-builder.service";

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

    const url = this.#urlBuilder.build("api/planentry");

    return this.#httpClient
      .get<PlanEntryModel[]>(url, params);
  }
}
