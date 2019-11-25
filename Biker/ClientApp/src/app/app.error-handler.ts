import { ErrorHandler, Inject, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export class AppErrorHandler implements ErrorHandler {

  constructor(@Inject(ToastrService) private toastr: ToastrService) {
  }

  handleError(error: any): void {
    this.toastr.error('An unexpected error happened.', 'Error', {
      timeOut: 2000,
      closeButton: true,
      progressBar: true,
      progressAnimation: 'increasing'
    });
  }
}
