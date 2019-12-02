import { BikeService } from '../services/bike.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  templateUrl: 'view-bike.component.html'
})
export class ViewBikeComponent implements OnInit {
  bike: any;
  bikeId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bikeService: BikeService) {

    this.route.params.subscribe(p => {
      this.bikeId = +p['id'];
      if (isNaN(this.bikeId) || this.bikeId <= 0) {
        router.navigate(['/bikes']);
        return;
      }
    });
  }

  ngOnInit() {
    this.bikeService.getBike(this.bikeId)
      .subscribe(
        b => this.bike = b,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/bikes']);
            return;
          }
        });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.bikeService.delete(this.bike.id)
        .subscribe(x => {
          this.router.navigate(['/bikes']);
        });
    }
  }
} 
