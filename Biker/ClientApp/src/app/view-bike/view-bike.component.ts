import { BikeService } from '../services/bike.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PhotoService } from '../services/photo.service';

@Component({
  templateUrl: 'view-bike.component.html'
})
export class ViewBikeComponent implements OnInit {
  bike: any;
  bikeId: number;
  @ViewChild('fileInput', { static: false }) fileInput: ElementRef;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bikeService: BikeService,
    private photoService: PhotoService) {

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

  uploadPhoto() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.photoService.upload(this.bikeId, nativeElement.files[0])
      .subscribe(x => console.log(x));
  }
} 
