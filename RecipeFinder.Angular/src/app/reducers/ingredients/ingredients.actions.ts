import { createAction, props } from '@ngrx/store';
import { Ingredient, IngredientForm } from '../../models/ingredient.model';

export const loadIngredients = createAction('[Ingredients] Load Ingredients');
export const loadIngredientsSuccess = createAction(
  '[Ingredients] Load Ingredients Success',
  props<{ ingredients: Ingredient[] }>()
);
export const loadIngredientsFailure = createAction(
  '[Ingredients] Load Ingredients Failure',
  props<{ error: any }>()
);
export const addIngredient = createAction(
  '[Ingredients] Add Ingredient',
  props<{ ingredientForm: IngredientForm }>()
);
export const addIngredientSuccess = createAction(
  '[Ingredients] Add Ingredient Success',
  props<{ ingredient: Ingredient }>()
);
export const addIngredientFailure = createAction(
  '[Ingredients] Add Ingredient Failure',
  props<{ error: any }>()
);
export const deleteIngredient = createAction(
  '[Ingredients] Delete Ingredient',
  props<{ ingredientId: number }>()
);
export const deleteIngredientSuccess = createAction(
  '[Ingredients] Delete Ingredient Success',
  props<{ ingredientId: number }>()
);
export const deleteIngredientFailure = createAction(
  '[Ingredients] Delete Ingredient Failure',
  props<{ error: any }>()
);
export const updateIngredient = createAction(
  '[Ingredients] Update Ingredient',
  props<{ ingredient: Ingredient }>()
);
export const updateIngredientSuccess = createAction(
  '[Ingredients] Update Ingredient Success',
  props<{ ingredient: Ingredient }>()
);
export const updateIngredientFailure = createAction(
  '[Ingredients] Update Ingredient Failure',
  props<{ error: any }>()
);
