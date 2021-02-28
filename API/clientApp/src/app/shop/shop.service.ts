import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from } from 'rxjs';
import { IBrands } from '../shared/models/Brands';
import { IPagination } from '../shared/models/Pagination';
import { ITypes } from '../shared/models/productTypes';
import { map } from 'rxjs/operators';
import { shopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/Product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:44430/api/';

  public getProducts(shopparms: shopParams) {

    let params = new HttpParams();
    if (shopparms.brandId !==0 ) {
      params = params.append('brandId', shopparms.brandId.toString());
    }
    if (shopparms.typeId !==0) {
      params = params.append('typeId', shopparms.typeId.toString());
    }

    params = params.append('pageIndex', shopparms.pageNumber.toString());
    params = params.append('pageSize', shopparms.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'Product', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
    );
  }
     //This is important to understand: when you call .map() method inside .pipe(), you're not getting a
    //single product in this case, you get the whole products array, pushed to Observable. Because you map
    //the values, that are stored in the Observable - the values of type: < product[] >.
    //you requested the function to return in form of observable (array of products) 
  

  public getBrands() {

    return this.http.get<IBrands[]>(this.baseUrl +'Product/ProductBrands');
  }

  public getProductTypes() {

    return this.http.get<ITypes[]>(this.baseUrl + 'Product/ProductTypes');
  }

  public getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'Product/' + id);
  }

}
