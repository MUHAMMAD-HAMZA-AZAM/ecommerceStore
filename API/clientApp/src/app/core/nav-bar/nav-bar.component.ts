import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../../account/account.service';
import { IUser } from '../../shared/models/address';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  currentUser$: Observable<IUser>;
  constructor(private accountservice: AccountService) { }

  ngOnInit(){
    this.currentUser$ = this.accountservice.currentUser$;
  }
  public logOut() {
    this.accountservice.logout();
  }

}
