import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/models/iOrderToCreate';
import { OrdersService } from './orders.service';
import { error } from 'protractor';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders: IOrder[];

  constructor(private ordersService: OrdersService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.ordersService.getOrdersForUser().subscribe((orders: IOrder[]) => {
      this.orders = orders;
    }, error => {
      console.log('Logged Output: : OrdersComponent -> getOrders -> error', error);
    });
  }
}
