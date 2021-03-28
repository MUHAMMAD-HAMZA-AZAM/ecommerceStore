import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from '../../basket/basket.service';
import { IProduct } from '../../shared/models/Product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  public productView: IProduct;
  public product: IProduct;
  public quantity: number = 1;


  constructor(public shopService: ShopService, private activateRoute: ActivatedRoute, private basketService: BasketService) { }

  ngOnInit(): void {
    this.getProductDetails();
  }

  public additemtoBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  public increment() {
    this.quantity++;
  }

  public decrement() {
    if (this.quantity > 1 ) {
      this.quantity--;          
    }
    
  }

  public getProductDetails() {
    this.shopService.getProduct(+ this.activateRoute.snapshot.paramMap.get('id')).subscribe(product => {
      product = this.productView;
    }, error => {

        console.log(error);
    });
  }

}
