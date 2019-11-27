import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { Bike, KeyValuePair } from '../models/bike';

@Component({
  templateUrl: 'bike-list.component.html'
})
export class BikeListComponent implements OnInit {
  bikes: Bike[];
  allBikes: Bike[];
  makes: KeyValuePair[];
  filter: any = {};

  constructor(private bikeService: BikeService) { }

  ngOnInit() {
    this.bikeService.getBikes()
      .subscribe(bikes => this.bikes = this.allBikes = bikes);

    this.bikeService.getMakes()
      .subscribe(makes => this.makes = makes);
  }

  onFilterChange() {
    var bikes = this.allBikes;

    if (this.filter.makeId)
      bikes = bikes.filter(b => b.make.id == this.filter.makeId);

    //if (this.filter.modelId)
    //  bikes = bikes.filter(b => b.model.id == this.filter.modelId);

    this.bikes = bikes;
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }
} 
