import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AsyncValidatorFn } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { timer, of } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder, private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, Validators.required],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')], //Sync Validators
        [this.validateEmailNotTaken()]// Aasync validators will be executed only if Sync validators are true
      ],
      password: [null, Validators.required]
    });
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(resp => {
      this.router.navigateByUrl('/shop');
    }, error => {
      this.errors = error.errors;
    });
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500) // Start execution after 500ms
        .pipe(
          switchMap(() => {
            const emailValue = control.value;
            if (!emailValue) {
              return of(null);
            }
            return this.accountService.checkEmailExists(emailValue).pipe(
              map(res => {
                return res ? { emailExists: true } : // Equivalent to 'Validators.required' and 'Validators.pattern'
                  null;
              }));
          })
        );
    };
  }
}
