import { BikeService } from '../services/bike.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PhotoService } from '../services/photo.service';
import { HttpEventType } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../services/user.service';

@Component({
  templateUrl: 'view-bike.component.html'
})
export class ViewBikeComponent implements OnInit {
  bike: any;
  bikeId: number;
  @ViewChild('fileInput', { static: false }) fileInput: ElementRef;
  photos: any;
  fileUploadProgress: string = '';
  subscription;
  loggedIn;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bikeService: BikeService,
    private photoService: PhotoService,
    private toastr: ToastrService,
    private userService: UserService) {

    this.route.params.subscribe(p => {
      this.bikeId = +p['id'];
      if (isNaN(this.bikeId) || this.bikeId <= 0) {
        router.navigate(['/bikes']);
        return;
      }
    });
  }

  ngOnInit() {
    this.loggedIn = this.userService.isloggedIn;

    this.bikeService.getBike(this.bikeId)
      .subscribe(
        b => this.bike = b,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/bikes']);
            return;
          }
        });

    this.photoService.getPhotos(this.bikeId)
      .subscribe(photos => this.photos = photos);
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
    var file = nativeElement.files[0];
    nativeElement.value = '';

    this.subscription = this.photoService.upload(this.bikeId, file)
      .subscribe(events => {
        if (events.type === HttpEventType.UploadProgress) {
          this.fileUploadProgress = Math.round(events.loaded / events.total * 100) + '%';
        } else if (events.type === HttpEventType.Response) {
          this.fileUploadProgress = '';
          this.photos.push(events.body);
        }
      }
        , err => {
          this.fileUploadProgress = '';
          this.toastr.error(err, 'Error', {
            timeOut: 2000,
            closeButton: true,
            progressBar: true,
            progressAnimation: 'increasing'
          });
        }
      );
  }
}
