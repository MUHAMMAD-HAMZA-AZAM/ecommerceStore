import { Component, OnInit } from '@angular/core';
import { error } from 'protractor';
import { IBrands } from '../shared/models/Brands';
import { IProduct } from '../shared/models/Product';
import { ITypes } from '../shared/models/productTypes';
import { shopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrands[];
  productTypes: ITypes[];
  totalCounts : number;
  shopParameters = new shopParams();

  constructor(public  _shopService: ShopService) { }

  ngOnInit() {
    debugger;
    this.populateProducts();
    this.populateBrands();
    this.populateTypes();
  }

  public populateProducts() {
    this._shopService.getProducts(this.shopParameters).subscribe(response => {
      this.products = response.data;
      this.shopParameters.pageNumber = response.pageIndex;
      this.shopParameters.pageSize = response.pageSize;
      this.totalCounts = response.count;
    },
      error => {
        console.log(error);
      });
  }

  public populateBrands() {
    this._shopService.getBrands().subscribe(response => {
      //...response is the seperator Operator
      this.brands = [{ id: 0, name: 'All' }, ...response];
    }, 
      error => {
        console.log(); 
    });
  }

  public populateTypes() {
    this._shopService.getProductTypes().subscribe(response => {
      //...response is the seperator Operator
      this.productTypes = [{ id: 0, name: 'All' }, ...response];
    },
      error => {
        console.log();
      
    });
  }

  public onBrandSelected(brandId: number) {
    this.shopParameters.brandId = brandId;
    this.populateProducts();
  }

  public onTypeSelected(typeId: number) {
    this.shopParameters.typeId = typeId;
    this.populateProducts();
  }
  public onPageChanged(event: any) {
    debugger;
    this.shopParameters.pageNumber = event.page;
    this.populateProducts();
  }
}
