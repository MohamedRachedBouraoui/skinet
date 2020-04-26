import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, ReplaySubject, of } from 'rxjs';
import { IUser } from '../shared/models/iUser';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { IAddress } from '../shared/models/iAddress';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl + 'account/';

  /*
      It immediately emit its initial value (in this case its null !)
      and in the 'authGuard' we asked it to check the current value in this observable
      to decide what to to do so after an 'F5' the first value is value
      so it redirect the user to the login page even though we're already logged in !
      that is why we have replaced it by 'ReplaySubject'
  */

  // private currentUserSource = new BehaviorSubject<IUser>(null);

  // With a 'ReplaySubject' the 'authGuard' will wait until this 'Subject' will have a value
  private currentUserSource = new ReplaySubject<IUser>(1); // 1== cache ONE USER
  currentUser$ = this.currentUserSource.asObservable();

  private readonly TOKEN = 'token';

  constructor(private httpClient: HttpClient,
    private router: Router) { }

  // getCurrentUserValue() {
  //   return this.currentUserSource.value;
  // }

  loadCurrentUser() {

    const token = localStorage.getItem(this.TOKEN);
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.httpClient.get(this.baseUrl, { headers }).pipe(
      map((user: IUser) => {
        this.setCurrentUser(user);
      }));
  }

  login(values: any) {
    return this.httpClient.post(this.baseUrl + 'login', values).pipe(
      map((user: IUser) => {
        this.setCurrentUser(user);
      }));
  }

  register(values: any) {
    return this.httpClient.post(this.baseUrl + 'register', values).pipe(
      map((user: IUser) => {
        this.setCurrentUser(user);
      }));
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.httpClient.get(this.baseUrl + 'emailexists?email=' + email);
  }

  getUserAddress() {
    return this.httpClient.get<IAddress>(this.baseUrl + 'address');
  }

  updateUserAddress(address: IAddress) {
    return this.httpClient.put<IAddress>(this.baseUrl + 'address', address);
  }

  private setCurrentUser(user: IUser) {
    if (user) {
      localStorage.setItem('token', user.token);
      this.currentUserSource.next(user);
    }
  }
}
