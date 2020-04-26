import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  get addressForm() {
    return this.checkoutForm.get('addressForm');
  }
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  saveUserAddress() {
    this.accountService.updateUserAddress(this.addressForm.value).subscribe(() => {
      this.toastr.success('Address saved')
    }, error => {

      console.log('Logged Output: : CheckoutAddressComponent -> saveUserAddress -> error', error);
      this.toastr.error(error.message);
    });
  }
}
