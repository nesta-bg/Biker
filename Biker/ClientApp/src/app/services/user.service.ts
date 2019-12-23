import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly myAppUrl: string;

  constructor(private http: HttpClient) { 
    this.myAppUrl = environment.appUrl;
  }

  register(formModel) {
    var body = {
      UserName: formModel.value.UserName,
      Email: formModel.value.Email,
      FullName: formModel.value.FullName,
      Password: formModel.value.Passwords.Password
    };
    return this.http.post(this.myAppUrl + 'api/AppUsers/Register', body);
  }

}
