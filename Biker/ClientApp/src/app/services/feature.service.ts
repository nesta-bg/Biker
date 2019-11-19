import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  myAppUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.myAppUrl = "https://localhost:44386/";
  }

  getFeatures() {
    return this._httpClient.get(this.myAppUrl + "api/features");
  }
}
