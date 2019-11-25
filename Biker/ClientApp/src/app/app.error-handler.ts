import { ErrorHandler, Inject, isDevMode } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import * as Sentry from "@sentry/browser";

export class AppErrorHandler implements ErrorHandler {

  constructor(@Inject(ToastrService) private toastr: ToastrService) {
  }

  handleError(error: any): void {
    if (!isDevMode())
      Sentry.captureException(error.originalError || error);
    else
      throw error;

    this.toastr.error('An unexpected error happened.', 'Error', {
      timeOut: 2000,
      closeButton: true,
      progressBar: true,
      progressAnimation: 'increasing'
    });
  }
}
