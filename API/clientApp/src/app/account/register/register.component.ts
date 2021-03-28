import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;

  constructor(private fb: FormBuilder, private service: AccountService, private router: Router) { }

  ngOnInit() {

    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern('^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$')]],
      password: ['', [Validators.required, Validators.pattern('^ ((\+ 92) | (0092)) -{ 0, 1}\d{ 3 } -{ 0, 1}\d{ 7 } $ |^\d{ 11 } $ |^\d{ 4 } -\d{ 7 } $')]],
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  public registerBtn() {
    if (this.registerForm.valid) {
      this.service.login(this.registerForm.value).subscribe(result => {
        this.router.navigateByUrl('/shop');
      }, error => {
        console.log(error);
      });
    }
    else {
      this.registerForm.markAllAsTouched();
    }
  }

}
