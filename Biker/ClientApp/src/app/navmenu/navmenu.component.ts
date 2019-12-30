import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './../services/user.service';

@Component({
  selector: 'nav-menu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
  loggedIn;
  userRole;

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.loggedIn = this.service.isloggedIn;
    if(this.loggedIn) {
      this.userRole = this.service.getUserRole();
    }

    this.service.isLoggedInSubject.subscribe(status => {
      this.loggedIn = status; 
      this.userRole = this.service.getUserRole();
    })
  }

  onLogout() {
    localStorage.removeItem('token');
    this.loggedIn = false;
    this.userRole = null;
    this.router.navigate(['/user/login']);
  }

}
