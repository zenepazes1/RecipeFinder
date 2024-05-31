import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import { selectFavoriteRecipes } from '../../reducers/favorite-recipes/favorite-recipes.select';
import { Observable, Subscription, map } from 'rxjs';
import { FullReceipt, Receipt } from '../../models/receipt.model';
import { ReceiptsService } from '../../services/receipts.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RecipeCardComponent } from '../../components/recipe-card/recipe-card.component';
import { loadFavoriteRecipes } from '../../reducers/favorite-recipes/favorite-recipes.actions';
import { RegisterService } from '../../services/register.service';
import { selectUserId } from '../../reducers/user/user.selector';

@Component({
  selector: 'app-favorite',
  standalone: true,
  imports: [CommonModule, MatIconModule, RecipeCardComponent],
  templateUrl: './favorite.component.html',
  styleUrl: './favorite.component.scss',
})
export class FavoriteComponent implements OnDestroy {
  isLoggedIn$: Observable<string> = this.store.pipe(select(selectUserId));
  isLoggedInSub: Subscription;
  favoriteRecipes$: Observable<Receipt[]> = this.store.pipe(
    select(selectFavoriteRecipes)
  );
  favoriteFullRecipes$: Observable<FullReceipt[]> = this.favoriteRecipes$.pipe(
    map((recipes) => {
      return recipes.map((recipe) => {
        return {
          ...recipe,
          isFavorite: true,
        };
      });
    })
  );

  constructor(
    private store: Store<State>,
    private registerService: RegisterService
  ) {
    this.store.dispatch(loadFavoriteRecipes());
    this.isLoggedInSub = this.isLoggedIn$.subscribe((userId) => {
      if (!userId) {
        this.registerService.openLoginDialog();
      }
    });
  }

  ngOnDestroy(): void {
    this.isLoggedInSub.unsubscribe();
  }
}
