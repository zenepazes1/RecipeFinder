import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CategoriesState } from "../../models/category.model";

export const selectCategoriesState = createFeatureSelector<CategoriesState>('categories');
export const selectCategories = createSelector(selectCategoriesState, (categoriesState: CategoriesState) => categoriesState.categories);