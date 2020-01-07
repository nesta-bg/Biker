import { Component, OnInit } from '@angular/core';
import { BikeService } from '../services/bike.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { SaveBike, Bike } from '../models/bike';
import * as _ from "underscore";
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './bike-form.component.html',
  styleUrls: ['./bike-form.component.css']
})
export class BikeFormComponent implements OnInit {
  title: string = 'New Bike';
  makes: any;
  models: any;
  features: any;
  bike: SaveBike = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: null,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(
    private bikeService: BikeService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {

    this.route.params
      .subscribe(p => {
        // + convert to number
        this.bike.id = +p['id'] || 0;
      });
  }

  ngOnInit() {
    var sources = [
      this.bikeService.getMakes(),
      this.bikeService.getFeatures()
    ];

    if (this.bike.id) {
      sources.push(this.bikeService.getBike(this.bike.id));
      this.title = 'Edit Bike';
    }

    forkJoin(sources)
      .subscribe(data => {
        this.makes = data[0];
        this.features = data[1];
        if (this.bike.id) {
          this.setBike(data[2] as Bike);
          this.populateModels();
        }
      }, err => {
        if (err.status == 404)
          this.router.navigate(['/home']);
      });
  }

  private setBike(b: Bike) {
    this.bike.id = b.id;
    this.bike.makeId = b.make.id;
    this.bike.modelId = b.model.id;
    this.bike.isRegistered = b.isRegistered;
    this.bike.contact = b.contact;
    this.bike.features = _.pluck(b.features, 'id');
  }

  onMakeChange() {
    this.populateModels();
    delete this.bike.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.bike.makeId);
    this.models = selectedMake ? selectedMake.models : [];
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
    // $ in result$ - this is an observable
    var result$ = (this.bike.id) ? this.bikeService.update(this.bike) : this.bikeService.create(this.bike); 

    result$
        .subscribe(x => {
          this.toastr.success('Data was sucessfully saved.', 'Success');
          this.router.navigate(['/bikes/', this.bike.id])
        });
  }
}
