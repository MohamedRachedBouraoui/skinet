import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService,
    private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {

    return this.accountService.currentUser$.pipe(
      // if there is a logged user then can activate else redirect to login page with in params the return url (for example the 'shop' url)
      map(auth => {
        if (auth) {
          return true;
        }

        this.router.navigate(['account/login'], { queryParams: { returnUrl: state.url } });
        // the 'returnUrl' obj will be available for the 'login' component when the user successfully login
      })
    );
  }

}
