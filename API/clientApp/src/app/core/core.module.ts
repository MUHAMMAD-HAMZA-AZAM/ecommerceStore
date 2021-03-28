import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';



@NgModule({
  declarations: [NavBarComponent, AppHeaderComponent],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot()
  ],
  exports: [NavBarComponent, AppHeaderComponent]
})
export class CoreModule { }
