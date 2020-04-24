import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';


  constructor(private basketService: BasketService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.getBasket();
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    this.accountService.loadCurrentUser().subscribe(() => {
      console.log('user loaded with success.');
    }, error => {
      console.log("Logged Output: : AppComponent -> loadCurrentUser -> error", error);
    });
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
