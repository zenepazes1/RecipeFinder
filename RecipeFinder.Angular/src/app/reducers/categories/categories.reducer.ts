import { createReducer, on } from "@ngrx/store";
import { CategoriesState } from "../../models/category.model";
import { addCategoryFailure, addCategorySuccess, deleteCategoryFailure, deleteCategorySuccess, loadCategoriesFailure, loadCategoriesSuccess, updateCategoryFailure, updateCategorySuccess } from "./categories.actions";

export const initialState: CategoriesState = {
  categories: [],
  error: null,
};

export const categoriesReducer = createReducer(
  initialState,
  on(loadCategoriesSuccess, (state, { categories }) => {
    return {
      ...state,
      categories,
      error: null,
    };
  }),
  on(loadCategoriesFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(addCategorySuccess, (state, { category }) => {
    return {
      ...state,
      categories: [...state.categories, category],
      error: null,
    };
  }),
  on(addCategoryFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(deleteCategorySuccess, (state, { categoryId }) => {
    return {
      ...state,
      categories: state.categories.filter((category) => category.categoryId !== categoryId),
      error: null,
    };
  }),
  on(deleteCategoryFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(updateCategorySuccess, (state, { category }) => {
    return {
      ...state,
      categories: state.categories.map((existingCategory) => {
        return existingCategory.categoryId === category.categoryId ? category : existingCategory;
      }),
      error: null,
    };
  }),
  on(updateCategoryFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
);