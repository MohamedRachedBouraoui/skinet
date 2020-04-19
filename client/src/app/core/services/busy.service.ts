import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Spinner } from 'ngx-spinner/lib/ngx-spinner.enum';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  busyRequestCount = 0;

  spinnerOpt: Spinner = {
    type: 'pacman',
    bdColor: 'rgba(255,255,255,0.7)',
    color: '#333333'
  };

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(): void {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, this.spinnerOpt);
  }
  idle(): void {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
    }
    this.spinnerService.hide();
  }
}
