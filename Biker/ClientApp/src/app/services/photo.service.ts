import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  private readonly myAppUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.myAppUrl = environment.appUrl;
  }

  upload(bikeId, photo) {
    var formData = new FormData();
    formData.append('file', photo);
    return this._httpClient.post(`${this.myAppUrl}api/bikes/${bikeId}/photos`, formData);
  }
}



