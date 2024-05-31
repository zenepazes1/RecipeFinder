import { createFeatureSelector, createSelector } from '@ngrx/store';
import { FavoriteRecipesState } from './favorite-recipes.reducer';

export const selectFavoriteRecipesState =
  createFeatureSelector<FavoriteRecipesState>('favoriteRecipes');

export const selectFavoriteRecipes = createSelector(
  selectFavoriteRecipesState,
  (favoriteRecipesState) => favoriteRecipesState.recipes
);
