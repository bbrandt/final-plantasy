import { Observer } from "rxjs";
import { Inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { WeatherForecastModel } from "./weather-forecast.model"

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  #httpClient: HttpClient;
  #baseUrl: string;

  constructor(
    httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string)
  {
    this.#httpClient = httpClient;
    this.#baseUrl = baseUrl;
  }

  public fetch(observer: Observer<WeatherForecastModel[]>): void {
    this.#httpClient
      .get<WeatherForecastModel[]>(this.#baseUrl + 'weatherforecast')
      .subscribe(observer);
  }
}
