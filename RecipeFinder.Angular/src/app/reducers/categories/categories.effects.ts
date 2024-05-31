import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { CategoriesService } from '../../services/categories.service';
import {
  addCategory,
  addCategoryFailure,
  addCategorySuccess,
  deleteCategory,
  deleteCategoryFailure,
  deleteCategorySuccess,
  loadCategories,
  loadCategoriesFailure,
  loadCategoriesSuccess,
  updateCategory,
  updateCategoryFailure,
  updateCategorySuccess,
} from './categories.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { Category } from '../../models/category.model';

@Injectable()
export class CategoriesEffects {
  constructor(
    private actions$: Actions,
    private categoriesService: CategoriesService
  ) {}

  loadCategories$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadCategories),
      switchMap(() =>
        this.categoriesService.getCategories().pipe(
          map((response: Category[]) => {
            return loadCategoriesSuccess({ categories: response });
          }),
          catchError((error: any) => {
            return of(loadCategoriesFailure({ error }));
          })
        )
      )
    )
  );

  addCategory$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addCategory),
      switchMap((action) =>
        this.categoriesService.addCategory(action.categoryForm).pipe(
          map((response: Category) => {
            return addCategorySuccess({ category: response });
          }),
          catchError((error: any) => {
            return of(addCategoryFailure({ error }));
          })
        )
      )
    )
  );

  deleteCategory$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteCategory),
      switchMap((action) =>
        this.categoriesService.deleteCategory(action.categoryId).pipe(
          map(() => {
            return deleteCategorySuccess({ categoryId: action.categoryId });
          }),
          catchError((error: any) => {
            return of(deleteCategoryFailure({ error }));
          })
        )
      )
    )
  );

  updateCategory$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateCategory),
      switchMap((action) =>
        this.categoriesService.updateCategory(action.category).pipe(
          map((response: any) => {
            return updateCategorySuccess({ category: action.category });
          }),
          catchError((error: any) => {
            return of(updateCategoryFailure({ error }));
          })
        )
      )
    )
  );
}
