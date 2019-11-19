import { Component, OnInit } from '@angular/core';
import { MakeService } from '../services/make.service';

@Component({
  templateUrl: './bike-form.component.html',
  styleUrls: ['./bike-form.component.css']
})
export class BikeFormComponent implements OnInit {
  makes;

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes()
      .subscribe(makes => {
        this.makes = makes
        console.log("MAKES", this.makes);
      });

    
  }

}
