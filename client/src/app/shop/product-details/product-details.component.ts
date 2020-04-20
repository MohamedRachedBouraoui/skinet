import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/iProduct';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;
  quantity = 1;

  constructor(private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService) {

    this.bcService.set('@productDetails', ''); // so we don't show the product-Id as default value while loading the product it self
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {

    const productId = +this.activatedRoute.snapshot.paramMap.get('id');
    this.shopService.getProduct(productId).subscribe((product: IProduct) => {

      this.bcService.set('@productDetails', product.name);
      this.product = product;

    }, error => {
      console.log(error);
    });
  }

  addItemToBasket(): void {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
