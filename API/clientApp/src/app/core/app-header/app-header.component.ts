import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss']
})
export class AppHeaderComponent implements OnInit {

  constructor(private bcSevice: BreadcrumbService) { }

  breadcrumb$: Observable<any[]>;

  ngOnInit(): void {
    this.breadcrumb$ = this.bcSevice.breadcrumbs$;
  }

}
