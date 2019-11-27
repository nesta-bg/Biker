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
    this.bikeService.getBikes()
      .subscribe(bikes => this.bikes = bikes);

    this.bikeService.getMakes()
      .subscribe(makes => this.makes = makes);
  }

  onFilterChange() {
  }
} 
