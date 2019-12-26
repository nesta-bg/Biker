import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from '../../environments/environment';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly myAppUrl: string;
  isLoggedInSubject: Subject<boolean> = new Subject();
  
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

  login(formData) {
    return this.http.post(this.myAppUrl + 'api/AppUsers/Login', formData);
  }

   // public get isloggedIn(): boolean{
  //   return localStorage.getItem('token') !==  null;
  // }
  isLoggedIn(val: boolean){
    this.isLoggedInSubject.next(val);
  }

  getUserProfile() {
    var tokenHeader = new HttpHeaders({'Authorization': 'Bearer ' + localStorage.getItem('token') });
    return this.http.get(this.myAppUrl + 'api/AppUsers/UserProfile', {headers: tokenHeader});
  }

}