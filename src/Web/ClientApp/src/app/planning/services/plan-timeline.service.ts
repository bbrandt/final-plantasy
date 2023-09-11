import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
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
    const datepipe = new DatePipe('en-US')

    const endDateParameter = datepipe.transform(endDate, 'dd-MMM-YYYY');

    const url = this.#urlBuilder.build(`api/plan-timeline/${endDateParameter}`);

    return this.#httpClient
      .get<PlanTimelineModel>(url);
  }
}
