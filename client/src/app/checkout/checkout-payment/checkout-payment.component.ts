import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrderToCreate } from 'src/app/shared/models/iOrderToCreate';
import { Router, NavigationExtras } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  get deliveryForm() {
    return this.checkoutForm.get('deliveryForm');
  }

  get addressForm() {
    return this.checkoutForm.get('addressForm');
  }

  constructor(private basketService: BasketService, private checkoutService: CheckoutService
    , private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

  submitOrder() {

    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.buildOrderToCreate(basket);
    this.checkoutService.createOrder(orderToCreate).subscribe((order: IOrderToCreate) => {

      console.log('Logged Output: : CheckoutPaymentComponent -> submitOrder -> order', order);

      this.toastr.success('Order Created successfully');
      this.basketService.deleteLocalBasket(basket.id);

      const navigationExtras: NavigationExtras = { state: order };
      this.router.navigate(['checkout/success'], navigationExtras);
    }, error => {
      console.log('Logged Output: : CheckoutPaymentComponent -> submitOrder -> error', error);
      this.toastr.error(error.message);
    });
  }

  private buildOrderToCreate(basket: IBasket): IOrderToCreate {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.deliveryForm.get('deliveryMethod').value,
      shipToAddress: this.addressForm.value
    };
  }
}
