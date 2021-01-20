import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { HttpClient} from '@angular/common/http'
import { error } from '@angular/compiler/src/util';
import { IProduct } from './models/Product';
import { IPagination } from './models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient) { }

  public products: IProduct[];

  ngOnInit(): void  {
    debugger;
    this.http.get('http://localhost:10976/api/Product?PageIndex=1&PageSize=1&BrandId=1&TypeId=1').subscribe((response: IPagination) => {

      this.products = response.data;
      console.log(this.products);

    }, error => {
        console.log(error);
    })
  }

  title = 'clientApp';
}
