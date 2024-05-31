import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { FavoriteRecipesService } from "../../services/favorite-recipes.service";
import { addFavoriteRecipe, addFavoriteRecipeFailure, addFavoriteRecipeSuccess, loadFavoriteRecipes, loadFavoriteRecipesFailure, loadFavoriteRecipesSuccess, removeFavoriteRecipe, removeFavoriteRecipeFailure, removeFavoriteRecipeSuccess } from "./favorite-recipes.actions";
import { catchError, map, of, switchMap } from "rxjs";
import { Receipt } from "../../models/receipt.model";

@Injectable()
export class FavoriteRecipesEffects {
  constructor(private actions$: Actions, private favoriteRecipesService: FavoriteRecipesService) {}

  loadFavoriteRecipes$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadFavoriteRecipes),
      switchMap(() =>
        this.favoriteRecipesService.getFavoriteRecipes().pipe(
          map((response: Receipt[]) => {
            return loadFavoriteRecipesSuccess({ favoriteRecipes: response });
          }),
          catchError((error: any) => {
            return of(loadFavoriteRecipesFailure({ error }));
          })
        )
      )
    )
  );

  addFavoriteRecipe$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addFavoriteRecipe),
      switchMap((action) =>
        this.favoriteRecipesService.addFavoriteRecipe(action.favoriteRecipe.recipeId).pipe(
          map((response: any) => {
            return addFavoriteRecipeSuccess({ favoriteRecipe: action.favoriteRecipe })
          }),
          catchError((error: any) => {
            return of(addFavoriteRecipeFailure({ error }))
          })
        )
      )
    )
  )

  removeFavoriteRecipe$ = createEffect(() =>
    this.actions$.pipe(
      ofType(removeFavoriteRecipe),
      switchMap((action) =>
        this.favoriteRecipesService.removeFavoriteRecipe(action.favoriteRecipe.recipeId).pipe(
          map((response: any) => {
            return removeFavoriteRecipeSuccess({favoriteRecipe: action.favoriteRecipe})
          }),
          catchError((error: any) => {
            return of(removeFavoriteRecipeFailure({error}))
          })
        )
      )
    )
  )
}