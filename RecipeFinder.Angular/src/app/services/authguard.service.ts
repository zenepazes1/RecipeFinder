import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { State } from '../reducers';

@Injectable()
export class AuthGuardService {
  constructor(private store: Store<State>, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let isLoggedIn = this.checkLogin();

    if (!isLoggedIn) {
      this.router.navigate(['/register']);
    }

    return isLoggedIn;
  }

  checkLogin() {
    let isLoggedIn = false;
    this.store.select('user').subscribe((user) => {
      isLoggedIn = user.userId ? true : false;
    });

    return isLoggedIn;
  }
}
