import { Injectable, Inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlBuilderService {
  readonly #baseUrl: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.#baseUrl = baseUrl;
  }

  public build(segments: string): string {
    return this.#baseUrl + segments;
  }
}
