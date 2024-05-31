import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IngredientsState } from "../../models/ingredient.model";

export const selectIngredientsState = createFeatureSelector<IngredientsState>('ingredients');

export const selectIngredients = createSelector(selectIngredientsState, (ingredientsState: IngredientsState) => ingredientsState.ingredients);