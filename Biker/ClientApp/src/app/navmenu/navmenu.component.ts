import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './../services/user.service';

@Component({
  selector: 'nav-menu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
  loggedIn = false;

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.isLoggedInSubject.subscribe(status => {
      this.loggedIn = status; 
    })
  }

  onLogout() {
    localStorage.removeItem('token');
    this.loggedIn = false;
    this.router.navigate(['/user/login']);
  }

}
