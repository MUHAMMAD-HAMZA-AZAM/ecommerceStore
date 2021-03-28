import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment.prod';
import { Basket, IBasket, IBasketItems } from '../shared/models/Basket';
import { IProduct } from '../shared/models/Product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.production;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();
  constructor(private http: HttpClient) { }
   getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket?id=' + id).pipe(
      map((basket: IBasket) => {
      })
    );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);

    }, error => {
        console.log(error);
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd = this.mapProductItemToBasket(item, quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdatebasket(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  private addOrUpdatebasket(items: IBasketItems[], itemsToAdd: IBasketItems, quantity: number): IBasketItems[]{
    const index = items.findIndex(i => i.id === itemsToAdd.id);
    if (index === -1) {
      itemsToAdd.quantity = quantity;
      items.push(itemsToAdd);
    }
    else {
      items[index].quantity += quantity;
    }
    return items;
  }
  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasket(item: IProduct, quantity: number): IBasketItems {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureURL,
      brand: item.productBrand,
      type: item.productType,
      quantity 
    }
  }

}
