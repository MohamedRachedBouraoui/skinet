import { v4 as guid } from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  productType: string;
}

export class Basket implements IBasket {
  id = guid();
  items: IBasketItem[] = [];

}
export interface IBasketTotals {
  subTotals: number;
  shipping: number;
  total: number;
}
