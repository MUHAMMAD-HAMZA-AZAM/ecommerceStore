import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { HttpClient} from '@angular/common/http'
import { error } from '@angular/compiler/src/util';
import { IProduct } from './shared/models/Product';
import { IPagination } from './shared/models/Pagination';
import { ShopService } from './shop/shop.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor() { }

  ngOnInit()  {
    
  }

  title = 'Big Bazaar';
}
