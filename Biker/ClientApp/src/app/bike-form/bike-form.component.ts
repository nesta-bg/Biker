import { Component, OnInit } from '@angular/core';
import { MakeService } from '../services/make.service';

@Component({
  templateUrl: './bike-form.component.html',
  styleUrls: ['./bike-form.component.css']
})
export class BikeFormComponent implements OnInit {
  makes: any;
  models: any;
  bike: any = {};

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes()
      .subscribe(makes => 
        this.makes = makes); 
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.bike.make);
    this.models = selectedMake ? selectedMake.models : [];
  }

}
