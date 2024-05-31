import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { FullReceipt, Receipt } from '../../models/receipt.model';
import { CommonModule } from '@angular/common';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import {
  addFavoriteRecipe,
  removeFavoriteRecipe,
} from '../../reducers/favorite-recipes/favorite-recipes.actions';
import { selectUserId } from '../../reducers/user/user.selector';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-recipe-card',
  standalone: true,
  imports: [RouterLink, MatButtonModule, MatIconModule, CommonModule],
  templateUrl: './recipe-card.component.html',
  styleUrl: './recipe-card.component.scss',
})
export class RecipeCardComponent {
  @Input() recipe: FullReceipt = {} as FullReceipt;
  isLoggedIn$: Observable<string> = this.store.pipe(select(selectUserId));

  constructor(private store: Store<State>) {}

  onFavoriteCheckClicked() {
    this.store.dispatch(
      addFavoriteRecipe({ favoriteRecipe: this.recipe as Receipt })
    );
  }

  onFavoriteUncheckClicked() {
    this.store.dispatch(
      removeFavoriteRecipe({ favoriteRecipe: this.recipe as Receipt })
    );
  }
}
