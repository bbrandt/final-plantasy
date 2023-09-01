import { Component } from '@angular/core';
import { Observer } from "rxjs";
import { WeatherForecastModel } from './weather-forecast.model';
import { WeatherService } from './weather.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  providers: [ WeatherService ]
})
export class FetchDataComponent {
  public forecasts: WeatherForecastModel[] = [];
  #weatherService: WeatherService;

  constructor(
    weatherService: WeatherService)
  {
    this.#weatherService = weatherService;
  }

  public ngOnInit(): void {
    this.initializeData();
  }

  private initializeData(): void {
    const observer: Observer<WeatherForecastModel[]> = {
      next: (result: WeatherForecastModel[]) => {
        this.forecasts = result;
      },
      error: (message: any) => {
        console.error(message);
      },
      complete: function (): void {
      }
    };

    this.#weatherService.fetch(observer);
  }
}

