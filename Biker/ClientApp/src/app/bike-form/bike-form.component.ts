import { Component, OnInit } from '@angular/core';
import { MakeService } from '../services/make.service';
import { FeatureService } from '../services/feature.service';

@Component({
  templateUrl: './bike-form.component.html',
  styleUrls: ['./bike-form.component.css']
})
export class BikeFormComponent implements OnInit {
  makes: any;
  models: any;
  features: any;
  bike: any = {};

  constructor(
    private makeService: MakeService,
    private featureService: FeatureService) { }

  ngOnInit() {
    this.makeService.getMakes()
      .subscribe(makes => 
        this.makes = makes);

    this.featureService.getFeatures()
      .subscribe(features =>
        this.features = features); 
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.bike.make);
    this.models = selectedMake ? selectedMake.models : [];
  }

}
