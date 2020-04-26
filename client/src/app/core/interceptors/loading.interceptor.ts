import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError, delay, finalize } from 'rxjs/operators';

import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    // Turn off the loading indicator when cheking for email

    if (req.url.includes('emailexists') === false) {

      this.busyService.busy();
    }
    return next.handle(req)
      .pipe(
        // delay(1000), // just to demonstrate the loading indicator
        finalize(() => {
          this.busyService.idle();
        })
      );
  }
}
