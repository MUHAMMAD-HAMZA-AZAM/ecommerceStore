import { uuid } from 'uuid'


export interface IBasket {
  id: string;
  items: IBasketItems[];
}

export interface IBasketItems {

  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;

}

export class Basket implements IBasket {
  id = uuid();
  items: IBasketItems[];

}
