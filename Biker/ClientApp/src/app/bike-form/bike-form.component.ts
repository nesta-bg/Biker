import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';

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

  constructor(
    private bikeService: BikeService,
    private route: ActivatedRoute,
    private router: Router) {

    this.route.params
      .subscribe(p => {
        // + convert to number
        this.bike.id = +p['id'];
      });
  }

  ngOnInit() {
    var sources = [
      this.bikeService.getMakes(),
      this.bikeService.getFeatures()
    ];

    if (this.bike.id)
      sources.push(this.bikeService.getBike(this.bike.id));

    forkJoin(sources)
      .subscribe(data => {
        this.makes = data[0];
        this.features = data[1];
        if (this.bike.id)
          this.bike = data[2];
      }, err => {
        if (err.status == 404) 
          this.router.navigate(['/home']);
      });
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
      .subscribe(x => console.log(x));
  }
}
