import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl + 'orders/';

  constructor(private httpClient: HttpClient) { }

  getOrdersForUser() {
    return this.httpClient.get(this.baseUrl);
  }

  getOrderDetailed(id: number) {
    return this.httpClient.get(this.baseUrl + id);
  }
}
