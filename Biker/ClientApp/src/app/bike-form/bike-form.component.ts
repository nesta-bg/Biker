import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';

@Component({
  templateUrl: './bike-form.component.html',
  styleUrls: ['./bike-form.component.css']
})
export class BikeFormComponent implements OnInit {
  makes: any;
  models: any;
  features: any;
  bike: any = {};

  constructor(private bikeService: BikeService) { }

  ngOnInit() {
    this.bikeService.getMakes()
      .subscribe(makes => 
        this.makes = makes);

    this.bikeService.getFeatures()
      .subscribe(features =>
        this.features = features); 
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.bike.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.bike.modelId;
  }

}
