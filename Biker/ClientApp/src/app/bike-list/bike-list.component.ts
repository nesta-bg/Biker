import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { Bike, KeyValuePair } from '../models/bike';

@Component({
  templateUrl: 'bike-list.component.html'
})
export class BikeListComponent implements OnInit {
  bikes: Bike[];
  makes: KeyValuePair[];
  query: any = {};

  constructor(private bikeService: BikeService) { }

  ngOnInit() {
    this.bikeService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateBikes();
  }

  private populateBikes() {
    this.bikeService.getBikes(this.query)
      .subscribe(bikes => this.bikes = bikes);
  }

  onFilterChange() {
    //this.query.modelId = 2;

    this.populateBikes();
  }

  resetFilter() {
    this.query = {};
    this.onFilterChange();
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
} 
