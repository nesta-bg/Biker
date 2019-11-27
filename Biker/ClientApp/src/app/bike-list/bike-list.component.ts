import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { Bike } from '../models/bike';

@Component({
  templateUrl: 'bike-list.component.html'
})
export class BikeListComponent implements OnInit {
  bikes: Bike[];

  constructor(private bikeService: BikeService) { }

  ngOnInit() {
    this.bikeService.getBikes()
      .subscribe(bikes => this.bikes = bikes);
  }
} 
