import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { KeyValuePair } from '../models/bike';
import { UserService } from '../services/user.service';

@Component({
  templateUrl: 'bike-list.component.html'
})
export class BikeListComponent implements OnInit {
  private readonly PAGE_SIZE = 6; 
  queryResult: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    {}
  ];
  loggedIn;

  constructor(private bikeService: BikeService, private userService: UserService) { }

  ngOnInit() {
    this.loggedIn = this.userService.isloggedIn;

    this.bikeService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateBikes();
  }

  private populateBikes() {
    this.bikeService.getBikes(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
    //this.query.modelId = 2;

    this.query.page = 1; 
    this.populateBikes();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateBikes();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateBikes();
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateBikes();
  }
} 
