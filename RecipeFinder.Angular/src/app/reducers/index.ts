import { isDevMode } from '@angular/core';
import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer,
} from '@ngrx/store';
import { UserState } from '../models/user.model';
import { userReducer } from './user/user.reducer';
import { IngredientsState } from '../models/ingredient.model';
import { ingredientsReducer } from './ingredients/ingredients.reducer';
import { UserEffects } from './user/user.effects';
import { IngredientsEffects } from './ingredients/ingredients.effects';
import { categoriesReducer } from './categories/categories.reducer';
import { CategoriesState } from '../models/category.model';
import { CategoriesEffects } from './categories/categories.effects';
import { ReceiptsState } from '../models/receipt.model';
import { receiptsReducer } from './receipts/receipts.reducer';
import { ReceiptsEffects } from './receipts/receipts.effects';
import { favoriteRecipesReducer, FavoriteRecipesState } from './favorite-recipes/favorite-recipes.reducer';
import { FavoriteRecipesEffects } from './favorite-recipes/favorite-recipes.effects';

export interface State {
  user: UserState;
  ingredients: IngredientsState;
  categories: CategoriesState;
  receipts: ReceiptsState;
  favoriteRecipes: FavoriteRecipesState;
}

export const reducers: ActionReducerMap<State> = {
  user: userReducer,
  ingredients: ingredientsReducer,
  categories: categoriesReducer,
  receipts: receiptsReducer,
  favoriteRecipes: favoriteRecipesReducer,
};

export const effects = [
  UserEffects,
  IngredientsEffects,
  CategoriesEffects,
  ReceiptsEffects,
  FavoriteRecipesEffects,
];

export const metaReducers: MetaReducer<State>[] = isDevMode() ? [] : [];
