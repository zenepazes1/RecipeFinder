import { createReducer, on } from '@ngrx/store';
import { Receipt } from '../../models/receipt.model';
import {
  addFavoriteRecipeFailure,
  addFavoriteRecipeSuccess,
  loadFavoriteRecipesFailure,
  loadFavoriteRecipesSuccess,
  removeFavoriteRecipeFailure,
  removeFavoriteRecipeSuccess,
} from './favorite-recipes.actions';

export interface FavoriteRecipesState {
  recipes: Receipt[];
  error: any;
}

export const initialState: FavoriteRecipesState = {
  recipes: [],
  error: null,
};

export const favoriteRecipesReducer = createReducer(
  initialState,
  on(loadFavoriteRecipesSuccess, (state, { favoriteRecipes }) => {
    return {
      ...state,
      recipes: favoriteRecipes,
      error: null,
    };
  }),
  on(loadFavoriteRecipesFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(addFavoriteRecipeSuccess, (state, { favoriteRecipe }) => {
    return {
      ...state,
      recipes: [...state.recipes, favoriteRecipe],
      error: null,
    };
  }),
  on(addFavoriteRecipeFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(removeFavoriteRecipeSuccess, (state, { favoriteRecipe }) => {
    return {
      ...state,
      recipes: state.recipes.filter(
        (recipe) => recipe.recipeId !== favoriteRecipe.recipeId
      ),
      error: null,
    };
  }),
  on(removeFavoriteRecipeFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  })
);
