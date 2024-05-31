import { createAction, props } from '@ngrx/store';
import { Receipt } from '../../models/receipt.model';

export const loadFavoriteRecipes = createAction(
  '[FavoriteRecipes] Load Favorite Recipes'
);
export const loadFavoriteRecipesSuccess = createAction(
  '[FavoriteRecipes] Load Favorite Recipes Success',
  props<{ favoriteRecipes: Receipt[] }>()
);
export const loadFavoriteRecipesFailure = createAction(
  '[FavoriteRecipes] Load Favorite Recipes Failure',
  props<{ error: any }>()
);

export const addFavoriteRecipe = createAction(
  '[FavoriteRecipes] Add Favorite Recipe',
  props<{ favoriteRecipe: Receipt }>()
);
export const addFavoriteRecipeSuccess = createAction(
  '[FavoriteRecipes] Add Favorite Recipe Success',
  props<{ favoriteRecipe: Receipt }>()
);
export const addFavoriteRecipeFailure = createAction(
  '[FavoriteRecipes] Add Favorite Recipe Failure',
  props<{ error: any }>()
);

export const removeFavoriteRecipe = createAction(
  '[FavoriteRecipes] Remove Favorite Recipe',
  props<{ favoriteRecipe: Receipt }>()
);
export const removeFavoriteRecipeSuccess = createAction(
  '[FavoriteRecipes] Remove Favorite Recipe Success',
  props<{ favoriteRecipe: Receipt }>()
);
export const removeFavoriteRecipeFailure = createAction(
  '[FavoriteRecipes] Remove Favorite Recipe Failure',
  props<{ error: any }>()
);
