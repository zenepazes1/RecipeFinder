import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { IngredientsService } from '../../services/ingredients.service';
import {
  addIngredient,
  addIngredientFailure,
  addIngredientSuccess,
  deleteIngredient,
  deleteIngredientFailure,
  deleteIngredientSuccess,
  loadIngredients,
  loadIngredientsFailure,
  loadIngredientsSuccess,
  updateIngredient,
  updateIngredientSuccess,
} from './ingredients.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { Ingredient } from '../../models/ingredient.model';

@Injectable()
export class IngredientsEffects {
  constructor(
    private actions$: Actions,
    private ingredientsService: IngredientsService
  ) {}

  loadIngredients$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadIngredients),
      switchMap(() =>
        this.ingredientsService.getIngredients().pipe(
          map((response: Ingredient[]) => {
            return loadIngredientsSuccess({ ingredients: response });
          }),
          catchError((error: any) => {
            return of(loadIngredientsFailure({ error }));
          })
        )
      )
    )
  );

  addIngredient$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addIngredient),
      switchMap((action) =>
        this.ingredientsService.addIngredient(action.ingredientForm).pipe(
          map((response: Ingredient) => {
            return addIngredientSuccess({ ingredient: response });
          }),
          catchError((error: any) => {
            return of(addIngredientFailure({ error }));
          })
        )
      )
    )
  );

  deleteIngredient$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteIngredient),
      switchMap((action) =>
        this.ingredientsService.deleteIngredient(action.ingredientId).pipe(
          map(() => {
            return deleteIngredientSuccess({
              ingredientId: action.ingredientId,
            });
          }),
          catchError((error: any) => {
            return of(deleteIngredientFailure({ error }));
          })
        )
      )
    )
  );

  updateIngredient$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateIngredient),
      switchMap((action) =>
        this.ingredientsService.updateIngredient(action.ingredient).pipe(
          map((response: any) => {
            return updateIngredientSuccess({ ingredient: action.ingredient });
          }),
          catchError((error: any) => {
            return of(addIngredientFailure({ error }));
          })
        )
      )
    )
  );
}
