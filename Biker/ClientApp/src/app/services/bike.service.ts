import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SaveBike } from '../models/bike';

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

  create(bike) {
    return this._httpClient.post(this.myAppUrl + "api/bikes", bike);
  }

  getBike(id) {
    return this._httpClient.get(this.myAppUrl + "api/bikes/" + id);
  }

  update(bike: SaveBike) {
    return this._httpClient.put(this.myAppUrl + "api/bikes/" + bike.id, bike);
  }

  delete(id) {
    return this._httpClient.delete(this.myAppUrl + "api/bikes/" + id);
  }
}








 

