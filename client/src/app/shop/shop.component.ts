import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IProduct } from '../shared/models/iProduct';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/iBrand';
import { IProductType } from '../shared/models/iProductType';
import { ShopParams } from '../shared/models/shopParams';
import { IPagination } from '../shared/models/iPagination';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  // We are using 'static = false' beacause we're surrounding our element
  // with *ngif in the html

  @ViewChild('searchBy', { static: false }) searchBy: ElementRef;

  products: IProduct[];
  brands: IBrand[];
  productTypes: IProductType[];

  shopParams = new ShopParams();

  sortOptions = [
    { name: 'By Name', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];


  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();


  }


  getProductTypes() {
    this.shopService.getProductTypes().subscribe(productTypes => {
      // will result on an array of 'ProductTYpe' with an extra fictive ProductTYpe (id=0) in first place
      // to allow the user select all the ProductTYpe
      this.productTypes = [{ id: 0, name: 'All' }, ...productTypes];
    }, error => {
      console.log(error);
    });
  }
  getBrands() {
    this.shopService.getBrands().subscribe(brands => {
      // will result on an array of 'Brands' with an extra fictive Brand (id=0) in first place
      // to allow the user select all the brands
      this.brands = [{ id: 0, name: 'All' }, ...brands];
    }, error => {
      console.log(error);
    });
  }


  private getProducts() {

    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.installPagination(response);
      this.getBrands();
      this.getProductTypes();
    }, error => {
      console.log(error);
    });
  }
  installPagination(response: IPagination) {
    // We must reset this value to 1 otherwise we'll have an exception from the page saying:
    // Expression has changed after it was checked .....
    this.shopParams.pageIndex = 1;

    this.shopParams.pageIndex = response.pageIndex;
    this.shopParams.pageSize = response.pageSize;
    this.shopParams.serverTotalProductsCount = response.count;
  }

  onBrandSelected(brandId: number): void {
    this.shopParams.brandId = brandId;

    this.getProducts();
  }

  onProductTypeSelected(productTypeId: number): void {
    this.shopParams.productTypeId = productTypeId;
    this.getProducts();
  }

  onSortSelected(sortBy: string): void {
    this.shopParams.sortBy = sortBy;
    this.getProducts();
  }
  onPageChanged(selectedPage: number): void {
    // According to the tuto, when we call 'getProducts()'
    // the pager trigger it's change event another time & and we come here and execute again
    // But I'm not facing this problem
    if (this.shopParams.pageIndex === selectedPage) {
      return;
    }

    this.shopParams.pageIndex = selectedPage;
    this.getProducts();
  }

  onSearch(): void {
    this.shopParams.searchBy = this.searchBy.nativeElement.value;
    this.getProducts();
  }

  onReset(): void {
    this.searchBy.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
