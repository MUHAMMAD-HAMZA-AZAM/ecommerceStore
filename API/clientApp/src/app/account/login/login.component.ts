import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  public returnUrl: string;
  constructor(private fb: FormBuilder, private service: AccountService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/shop';
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern('^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$')]],
      password: ['', [Validators.required, Validators.pattern('^ ((\+ 92) | (0092)) -{ 0, 1}\d{ 3 } -{ 0, 1}\d{ 7 } $ |^\d{ 11 } $ |^\d{ 4 } -\d{ 7 } $')]],
      remember: [false] 
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  public loginBtn() {
    if (this.loginForm.valid) {
      this.service.login(this.loginForm.value).subscribe(result => {
        this.router.navigateByUrl(this.returnUrl);
      }, error => {
          console.log(error);
      });
    }
    else {
      this.loginForm.markAllAsTouched();
    }
  }
}
