import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

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

    return this._httpClient.post(`${this.myAppUrl}api/bikes/${bikeId}/photos`, formData,
      {
        reportProgress: true,
        observe: 'events'
      })
      .pipe(
        catchError(this.handleError)
      )
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error: ${error.error}`;
    }

    return throwError(errorMessage);
  }

  getPhotos(bikeId) {
    return this._httpClient.get(`${this.myAppUrl}api/bikes/${bikeId}/photos`);
  }
}



