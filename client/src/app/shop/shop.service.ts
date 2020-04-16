import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IPagination } from '../shared/models/iPagination';
import { IBrand } from '../shared/models/iBrand';
import { IProductType } from '../shared/models/iProductType';
import { ShopParams } from '../shared/models/shopParams';
import { Observable } from 'rxjs';
import { IProduct } from '../shared/models/iProduct';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  productsBaseUrl = 'https://localhost:5001/api/products/';

  constructor(private httpClient: HttpClient) {

  }

  getProducts(shopParams: ShopParams) {
    const httpParams = this.setupHttpParams(shopParams);

    // this will give us the HttpResponse instead of the body's response
    const result = this.httpClient.get<IPagination>(this.productsBaseUrl, { observe: 'response', params: httpParams })
      .pipe(
        map(theHttpResponse => {
          console.log('Logged Output: : ShopService -> getProducts -> response', theHttpResponse);
          return theHttpResponse.body;
        }));
    return result;
  }

  private setupHttpParams(shopParams: ShopParams) {
    let httpParams = new HttpParams();
    httpParams = httpParams.append('pageIndex', shopParams.pageIndex.toString());
    httpParams = httpParams.append('pageSize', shopParams.pageSize.toString());
    httpParams = httpParams.append('sortBy', shopParams.sortBy);
    if (shopParams.brandId && shopParams.brandId > 0) {
      httpParams = httpParams.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.productTypeId && shopParams.productTypeId > 0) {
      httpParams = httpParams.append('typeId', shopParams.productTypeId.toString());
    }
    if (shopParams.searchBy && shopParams.searchBy !== '') {
      httpParams = httpParams.append('searchBy', shopParams.searchBy);
    }
    return httpParams;
  }

  getProduct(productId: number): Observable<IProduct> {

    return this.httpClient.get<IProduct>(this.productsBaseUrl + `${productId}`);
  }

  getBrands() {
    return this.httpClient.get<IBrand[]>(this.productsBaseUrl + 'brands');
  }

  getProductTypes() {
    return this.httpClient.get<IProductType[]>(this.productsBaseUrl + 'types');
  }

}
