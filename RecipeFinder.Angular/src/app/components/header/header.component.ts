import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import { Observable, Subscription, map } from 'rxjs';
import { UserState } from '../../models/user.model';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {
  selectUserId,
  selectUserRoles,
  selectUserState,
} from '../../reducers/user/user.selector';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    CommonModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnDestroy {
  isLoggedIn$: Observable<string> = this.store.pipe(select(selectUserId));
  userRole$: Observable<string> = this.store.pipe(
    select(selectUserRoles),
    map((roles) => roles[0])
  );
  userRoleSub: Subscription;
  userRole: string = '';

  constructor(private store: Store<State>, private router: Router) {
    this.userRoleSub = this.userRole$.subscribe((role) => {
      this.userRole = role;
    });
  }

  onProfileClick() {
    if (this.userRole === 'Admin') {
      this.router.navigate(['/admin']);
    } else {
      this.router.navigate(['/profile']);
    }
  }

  ngOnDestroy(): void {}
}
