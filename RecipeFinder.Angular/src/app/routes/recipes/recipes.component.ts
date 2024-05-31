import { Component } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import { loadReceipts } from '../../reducers/receipts/receipts.actions';
import { FullReceipt, Receipt } from '../../models/receipt.model';
import { selectReceipts } from '../../reducers/receipts/receipts.selector';
import {
  Observable,
  combineLatest,
  map,
  startWith,
  switchMap,
  withLatestFrom,
} from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { selectFavoriteRecipes } from '../../reducers/favorite-recipes/favorite-recipes.select';
import { loadFavoriteRecipes } from '../../reducers/favorite-recipes/favorite-recipes.actions';
import { selectUserId } from '../../reducers/user/user.selector';
import { RecipeCardComponent } from '../../components/recipe-card/recipe-card.component';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    RouterLink,
    MatIconModule,
    RecipeCardComponent,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
  ],
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.scss',
})
export class RecipesComponent {
  userId$: Observable<string> = this.store.pipe(select(selectUserId));
  receipts$: Observable<Receipt[]> = this.store.pipe(select(selectReceipts));
  nonUserReceipts$: Observable<Receipt[]> = this.receipts$.pipe(
    withLatestFrom(this.userId$),
    map(([receipts, userId]) => {
      return receipts.filter((receipt) => receipt.authorId !== userId);
    })
  );
  favoriteRecipes$: Observable<Receipt[]> = this.store.pipe(
    select(selectFavoriteRecipes)
  );
  fullRecipes$: Observable<FullReceipt[]> = combineLatest([
    this.nonUserReceipts$,
    this.favoriteRecipes$,
  ]).pipe(
    map(([recipes, favoriteRecipes]) => {
      let fullRecipes: FullReceipt[] = [];
      recipes.forEach((recipe) => {
        const fr = favoriteRecipes.find((fr) => fr.recipeId == recipe.recipeId);
        fullRecipes.push(
          fr
            ? { ...recipe, isFavorite: true }
            : { ...recipe, isFavorite: false }
        );
      });
      return fullRecipes;
    })
  );
  searchControl = new FormControl('');
  filteredRecipes$ = combineLatest([
    this.searchControl.valueChanges.pipe(startWith('')),
    this.fullRecipes$,
  ]).pipe(
    map(([search, recipes]) =>
      recipes.filter((recipe) =>
        recipe.title.toLowerCase().includes(search!.toLowerCase())
      )
    )
  );

  constructor(private store: Store<State>) {
    this.store.dispatch(loadReceipts());
    this.store.dispatch(loadFavoriteRecipes());
  }
}
