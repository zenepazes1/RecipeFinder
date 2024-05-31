import { Component, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import {
  selectUserId,
  selectUserState,
} from '../../reducers/user/user.selector';
import { Observable, Subscription, map, switchMap, withLatestFrom } from 'rxjs';
import { UserState } from '../../models/user.model';
import { CommonModule } from '@angular/common';
import { selectUserReceipts } from '../../reducers/receipts/receipts.selector';
import { Receipt } from '../../models/receipt.model';
import { Router, RouterLink } from '@angular/router';
import {
  deleteReceipt,
  loadReceipts,
} from '../../reducers/receipts/receipts.actions';
import { MatButtonModule } from '@angular/material/button';
import { ReceiptsService } from '../../services/receipts.service';
import { userLogOut } from '../../reducers/user/user.actions';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent {
  user$: Observable<UserState> = this.store.pipe(select(selectUserState));
  fullName$: Observable<string> = this.user$.pipe(
    select((user) => `${user.firstName} ${user.lastName}`)
  );
  userId$: Observable<string> = this.store.pipe(select(selectUserId));
  userRecipes$: Observable<Receipt[]> = this.store.pipe(
    withLatestFrom(this.userId$),
    switchMap(([_, userId]) =>
      this.store.select(selectUserReceipts({ userId }))
    )
  );

  constructor(
    private store: Store<State>,
    private router: Router,
    private receiptsService: ReceiptsService
  ) {
    this.store.dispatch(loadReceipts());
  }

  onRecipeClick(recipeId: number) {
    this.router.navigate([`/recipes/${recipeId}`]);
  }

  onDeleteClick(recipeId: number) {
    this.store.dispatch(deleteReceipt({ id: recipeId }));
  }
  onEditClick(recipe: Receipt) {
    this.receiptsService.openManageReceipt(recipe);
  }

  onAddReceiptClick() {
    this.receiptsService.openManageReceipt(null);
  }

  onLogOutClick() {
    this.store.dispatch(userLogOut());
    this.router.navigate(['/register']);
  }
}
