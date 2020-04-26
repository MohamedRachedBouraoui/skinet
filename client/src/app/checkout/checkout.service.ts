import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IDeliveryMethod } from '../shared/models/iDeliveryMethod';
import { IOrderToCreate } from '../shared/models/iOrderToCreate';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.apiUrl + 'orders/';

  constructor(private httpClient: HttpClient) { }

  createOrder(orderToCreate: IOrderToCreate) {
    return this.httpClient.post(this.baseUrl, orderToCreate);
  }

  getDeliveryMethods() {
    return this.httpClient.get(this.baseUrl + 'deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) => {
        return dm.sort((a, b) => b.price - a.price); // Sort by price Desc
      })
    );
  }
}
