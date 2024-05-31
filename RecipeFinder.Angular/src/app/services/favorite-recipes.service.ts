import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';
import { Observable, switchMap } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { selectUserId } from '../reducers/user/user.selector';
import { State } from '../reducers';
import { Receipt } from '../models/receipt.model';

@Injectable({
  providedIn: 'root',
})
export class FavoriteRecipesService {
  userId$: Observable<string> = this.store.pipe(select(selectUserId));

  constructor(
    private http: HttpClient,
    private backend: BackendService,
    private store: Store<State>
  ) {}

  getFavoriteRecipes(): Observable<Receipt[]> {
    return this.userId$.pipe(
      switchMap((userId) =>
        this.http.get<Receipt[]>(
          `${this.backend.url}/api/Recipes/favorites/${userId}`
        )
      )
    );
  }

  addFavoriteRecipe(recipeId: number): Observable<any> {
    return this.userId$.pipe(
      switchMap((userId) => {
        return this.http.post(
          `${this.backend.url}/api/Recipes/${recipeId}/favorites`,
          { userId: userId }
        );
      })
    );
  }

  removeFavoriteRecipe(recipeId: number): Observable<any> {
    return this.userId$.pipe(
      switchMap((userId) =>
        this.http.delete(
          `${this.backend.url}/api/Recipes/${recipeId}/favorites/${userId}`
        )
      )
    );
  }
}
