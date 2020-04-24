import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Subscription, Observable } from 'rxjs';
import { IBasket, IBasketItem, Basket, IBasketTotals } from '../shared/models/basket';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/models/iProduct';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl + 'basket';
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();


  constructor(private httpClient: HttpClient) { }

  getBasket(id: string): Observable<void> {
    // We will subscribe to this Observable in the Html in async mode
    return this.httpClient.get(this.baseUrl + '?id=' + id)
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          this.calculateTotals();
        })
      );
  }

  setBasket(basket: IBasket): Subscription {
    return this.httpClient.post(this.baseUrl, basket).subscribe((createdBasket: IBasket) => {

      this.basketSource.next(createdBasket);
      this.calculateTotals();
    }, error => {
      console.log('Logged Output: : BasketService -> setBasket -> error', error);
    });
  }

  getCurrentBasketValue(): IBasket {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1): void {

    // the ?? expression comes with Angular-9 because it uses "typescript": "~3.8.3"
    const basket = this.getCurrentBasketValue() ?? this.createBasket();

    let basketItem = basket.items.find(i => i.id === item.id);
    if (basketItem !== undefined) {
      basketItem.quantity += quantity;
    } else {
      basketItem = this.mapProductItemToBasketItem(item, quantity);
      basket.items.push(basketItem);
    }

    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasketItem): void {
    const basket = this.getCurrentBasketValue();
    const itemToUpdate = basket.items.find(itm => itm.id === item.id);

    if (itemToUpdate === undefined) {
      return;
    }

    itemToUpdate.quantity++;
    this.setBasket(basket);
  }
  decrementItemQuantity(item: IBasketItem): void {
    const basket = this.getCurrentBasketValue();
    const itemToUpdate = basket.items.find(itm => itm.id === item.id);

    if (itemToUpdate === undefined) {
      return;
    }

    if (itemToUpdate.quantity > 1) {
      itemToUpdate.quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(itemToUpdate);
    }
  }

  removeItemFromBasket(itemToRemove: IBasketItem) {
    const basket = this.getCurrentBasketValue();

    if (basket.items.find(itm => itm.id === itemToRemove.id)) {
      basket.items = basket.items.filter(itm => itm.id !== itemToRemove.id);
    }
    if (basket.items.length > 0) {
      this.setBasket(basket);
    } else {
      this.deleteBasket(basket);
    }
  }

  deleteBasket(basket: IBasket) {
    return this.httpClient.delete(this.baseUrl + '?id=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log('Logged Output: : BasketService -> deleteBasket -> error', error);

    });
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);

    return basket;

  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {

    return {
      id: item.id,
      productName: item.name,
      pictureUrl: item.pictureUrl,
      price: item.price,
      quantity,
      brand: item.productBrand,
      productType: item.productType
    };

  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();

    const shipping = 0;
    const subTotals = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subTotals + shipping;
    this.basketTotalSource.next({
      shipping,
      subTotals,
      total
    });
  }
}
