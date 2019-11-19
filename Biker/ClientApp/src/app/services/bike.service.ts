import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BikeService {
  myAppUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.myAppUrl = "https://localhost:44386/";
  }

  getMakes() {
    return this._httpClient.get(this.myAppUrl + "api/makes");
  }

  getFeatures() {
    return this._httpClient.get(this.myAppUrl + "api/features");
  }
}








 

