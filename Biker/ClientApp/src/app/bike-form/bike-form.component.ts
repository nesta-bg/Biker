import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './bike-form.component.html',
  styleUrls: ['./bike-form.component.css']
})
export class BikeFormComponent implements OnInit {
  makes: any;
  models: any;
  features: any;
  bike: any = {
    features: [],
    contact: {}
  };

  constructor(private bikeService: BikeService, private toastr: ToastrService) { }

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

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked)
      this.bike.features.push(featureId);
    else {
      var index = this.bike.features.indexOf(featureId);
      this.bike.features.splice(index, 1);
    }
  }

  submit() {
    this.bikeService.create(this.bike)
      .subscribe(
        x => console.log(x),
        err => {
          //server-side validation
          //if (err.status == 400) {
          //}

          this.toastr.error('An unexpected error happened.', 'Error', {
            timeOut: 2000,
            closeButton: true,
            progressBar: true,
            progressAnimation: 'increasing'
          });
        }
      );
  }
}
