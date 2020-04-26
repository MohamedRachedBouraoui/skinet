import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError, delay, finalize } from 'rxjs/operators';

import { BusyService } from '../services/busy.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private httpClient: HttpClient) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = localStorage.getItem('token');
    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
    return next.handle(req);
  }
}
