import { Component, OnInit } from '@angular/core';
import { IOrder } from 'src/app/shared/models/iOrderToCreate';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {

  order: IOrder;

  constructor(private route: ActivatedRoute, private breadcrumbService: BreadcrumbService,
    private ordersService: OrdersService) {
    this.breadcrumbService.set('@OrderDetailed', '');
  }

  ngOnInit(): void {
    const orderId = +this.route.snapshot.paramMap.get('id');
    this.ordersService.getOrderDetailed(orderId).subscribe((order: IOrder) => {
      this.order = order;
      this.breadcrumbService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
    }, error => {
      console.log('Logged Output: : OrderDetailedComponent -> ngOnInit -> error', error);
    });
  }

}