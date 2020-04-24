import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  returnUrl: string;
  constructor(private accountService: AccountService, private router: Router
    , private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    // the '.queryParams.returnUrl' will be initilized in the 'authGuard'

    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/shop';
    this.createLoginForm();

  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    const creds = this.loginForm.value;

    this.accountService.login(creds).subscribe(() => {
      this.router.navigateByUrl(this.returnUrl);
    }, error => {
      console.log("Logged Output: : LoginComponent -> onSubmit -> error", error);

    });
  }

}
