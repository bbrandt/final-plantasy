import { Injectable } from '@angular/core';
import { DateTime } from 'luxon';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UrlBuilderService } from './../../services/url-builder.service';
import { PlanTimelineModel } from './../model/plan-timeline.model';

@Injectable({
  providedIn: 'root'
})
export class PlanTimelineService {
  readonly #httpClient: HttpClient;
  readonly #urlBuilder: UrlBuilderService;

  constructor(
    httpClient: HttpClient,
    urlBuilder: UrlBuilderService) {
    this.#httpClient = httpClient;
    this.#urlBuilder = urlBuilder;
  }

  public getTimeline(endDate: Date): Observable<PlanTimelineModel> {
    const dateTime = DateTime.fromJSDate(endDate);

    const endDateParameter = dateTime.toISODate();

    const url = this.#urlBuilder.build(`api/plan-timeline/${endDateParameter}`);

    return this.#httpClient
      .get<PlanTimelineModel>(url);
  }
}
