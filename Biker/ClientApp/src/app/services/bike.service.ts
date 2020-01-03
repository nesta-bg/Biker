import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SaveBike, Bike, KeyValuePair } from '../models/bike';
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
    return this._httpClient.get<KeyValuePair[]>(this.myAppUrl + "api/makes");
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

  getBikes(filter) {
    return this._httpClient.get<Bike[]>(this.myAppUrl + this.bikesEndpoint + '?' + this.toQueryString(filter));
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }

  update(bike: SaveBike) {
    return this._httpClient.put(this.myAppUrl + this.bikesEndpoint + '/' + bike.id, bike);
  }

  delete(id) {
    return this._httpClient.delete(this.myAppUrl + this.bikesEndpoint + '/' + id);
  }

  getBikesGroupedByMake() {
    return this._httpClient.get(this.myAppUrl + this.bikesEndpoint + '/' + 'GroupByMake');
  }
}








 

