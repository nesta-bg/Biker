import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SaveBike, Bike } from '../models/bike';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BikeService {
  private readonly myAppUrl: string;
  private readonly bikesEndpoint: string; 
  
  constructor(private _httpClient: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.bikesEndpoint = "api/bikes";
  }

  getMakes() {
    return this._httpClient.get(this.myAppUrl + "api/makes");
  }

  getFeatures() {
    return this._httpClient.get(this.myAppUrl + "api/features");
  }

  create(bike) {
    return this._httpClient.post(this.myAppUrl + this.bikesEndpoint, bike);
  }

  getBike(id) {
    return this._httpClient.get(this.myAppUrl + this.bikesEndpoint + '/' + id);
  }

  getBikes() {
    return this._httpClient.get<Bike[]>(this.myAppUrl + this.bikesEndpoint);
  }

  update(bike: SaveBike) {
    return this._httpClient.put(this.myAppUrl + this.bikesEndpoint + '/' + bike.id, bike);
  }

  delete(id) {
    return this._httpClient.delete(this.myAppUrl + this.bikesEndpoint + '/' + id);
  }
}








 

