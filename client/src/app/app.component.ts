import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';


  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.getBasket();
  }

  getBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('Basket init.');
      }, error => {
        console.log("Logged Output: : ShopComponent -> getBasket -> error", error);

      });
    }
  }
}
