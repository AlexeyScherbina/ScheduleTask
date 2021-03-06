import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Config } from '../url.config';

import { Auth } from '../_models/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<Auth>;
  public currentUser: Observable<Auth>;

  constructor(private http: HttpClient, private router: Router) {
      this.currentUserSubject = new BehaviorSubject<Auth>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): Auth {
      return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    var grant_type = 'password';
    let body = new URLSearchParams();
    body.set('username', username);
    body.set('password', password);
    body.set('grant_type', grant_type);
      return this.http.post<any>(Config.baseUrl +  `/token`, body.toString())
          .pipe(map(user => {
              if (user && user.access_token) {
                  localStorage.setItem('currentUser', JSON.stringify(user));
                  this.currentUserSubject.next(user);
              }

              return user;
          }));
  }


  register(username: string, password: string){
    return this.http.post(Config.baseUrl +  `/api/Account/Register`,{Email: username, Password: password, ConfirmPassword: password});
  }

  logout() {
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
      this.router.navigate(['login']);
  }

}
