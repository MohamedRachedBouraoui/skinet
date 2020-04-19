import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(

      catchError(httpReponseError => {
        if (httpReponseError) {
          if (httpReponseError.status === 400) {

            if (httpReponseError.error.errors) { // then it's a Mpdel State Validation Error
              throw httpReponseError.error; // will handle the Model Validation errors in an other component
            } else {
              this.toastr.error(httpReponseError.error.message, '400');
            }
          }
          if (httpReponseError.status === 401) {
            this.toastr.error(httpReponseError.error.message, '401');
          }

          if (httpReponseError.status === 404) {
            this.router.navigateByUrl('/not-found');
          }
          if (httpReponseError.status === 500) {
            const navigationExtras: NavigationExtras = {
              state: { error: httpReponseError.error }
            };
            this.router.navigateByUrl('/server-error', navigationExtras);
          }
        }
        return throwError(httpReponseError);
      }));

  }

}
