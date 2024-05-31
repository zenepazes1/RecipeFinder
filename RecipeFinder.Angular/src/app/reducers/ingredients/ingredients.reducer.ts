import { createReducer, on } from "@ngrx/store";
import { Ingredient, IngredientsState } from "../../models/ingredient.model";
import { addIngredientFailure, addIngredientSuccess, deleteIngredientFailure, deleteIngredientSuccess, loadIngredientsFailure, loadIngredientsSuccess, updateIngredientFailure, updateIngredientSuccess } from "./ingredients.actions";

export const initialState: IngredientsState = {
  ingredients: [],
  error: null,
};

export const ingredientsReducer = createReducer(
  initialState,
  on(loadIngredientsSuccess, (state, {ingredients}) => {
    return {
      ...state,
      ingredients,
      error: null,
    };
  }),
  on(loadIngredientsFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(addIngredientSuccess, (state, { ingredient }) => {
    return {
      ...state,
      ingredients: [...state.ingredients, ingredient],
      error: null,
    };
  }),
  on(addIngredientFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(deleteIngredientSuccess, (state, { ingredientId }) => {
    return {
      ...state,
      ingredients: state.ingredients.filter((ingredient) => ingredient.ingredientId !== ingredientId),
      error: null,
    };
  }),
  on(deleteIngredientFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(updateIngredientSuccess, (state, { ingredient }) => {
    return {
      ...state,
      ingredients: state.ingredients.map((existingIngredient) => {
        return existingIngredient.ingredientId === ingredient.ingredientId ? ingredient : existingIngredient;
      }),
      error: null,
    };
  }),
  on(updateIngredientFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  })
);