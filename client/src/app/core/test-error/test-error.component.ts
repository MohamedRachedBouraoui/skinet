import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  productApiUrl = this.baseUrl + 'product/';
  buggyApiUrl = this.baseUrl + 'buggy/';

  validationErrors: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }
  get404Error() {
    this.http.get(this.buggyApiUrl + '42').subscribe(response => {

      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get500Error() {
    this.http.get(this.buggyApiUrl + 'servererror').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400Error() {
    this.http.get(this.buggyApiUrl + 'badrequest').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400ValidationError() {
    this.http.get(this.productApiUrl + 'fortytwo').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
      this.validationErrors = error.errors;
    });
  }

}
