import { Component, Inject } from '@angular/core';
import { Observer } from "rxjs";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;

    this.initializeData();
  }

  private initializeData(): void {
    const observer: Observer<WeatherForecast[]> = {
      next: (result: WeatherForecast[]) => {
        this.forecasts = result;
      },
      error: (message: any) => {
        console.error(message);
      },
      complete: function (): void {

      }
    };

    this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast')
      .subscribe(observer);
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
