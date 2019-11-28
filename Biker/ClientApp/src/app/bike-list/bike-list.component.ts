import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { Bike, KeyValuePair } from '../models/bike';

@Component({
  templateUrl: 'bike-list.component.html'
})
export class BikeListComponent implements OnInit {
  bikes: Bike[];
  makes: KeyValuePair[];
  filter: any = {};

  constructor(private bikeService: BikeService) { }

  ngOnInit() {
    this.bikeService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateBikes();
  }

  private populateBikes() {
    this.bikeService.getBikes(this.filter)
      .subscribe(bikes => this.bikes = bikes);
  }

  onFilterChange() {
    //this.filter.modelId = 2;

    this.populateBikes();
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }
} 
