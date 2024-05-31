import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { State } from '../../reducers';
import { userLogOut } from '../../reducers/user/user.actions';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [MatButtonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent {
  constructor(private router: Router, private store: Store<State>) {}

  onManageIngredientsClick() {
    this.router.navigate(['/ingredients']);
  }

  onManageCategoriesClick() {
    this.router.navigate(['/categories']);
  }

  onLogOutClick() {
    this.store.dispatch(userLogOut());
    this.router.navigate(['/register']);
  }
}
