import { Injectable } from '@angular/core';
import { BackendService } from './backend.service';
import { HttpClient } from '@angular/common/http';
import { Ingredient, IngredientForm } from '../models/ingredient.model';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ManageIngredientComponent } from '../components/manage-ingredient/manage-ingredient.component';

@Injectable({
  providedIn: 'root',
})
export class IngredientsService {
  constructor(
    private http: HttpClient,
    private backend: BackendService,
    public dialog: MatDialog
  ) {}

  public getIngredients(): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(`${this.backend.url}/api/Ingredients`);
  }

  public getIngredient(id: number): Observable<Ingredient> {
    return this.http.get<Ingredient>(
      `${this.backend.url}/api/Ingredients/${id}`
    );
  }

  public addIngredient(ingredientForm: IngredientForm): Observable<Ingredient> {
    return this.http.post<Ingredient>(
      `${this.backend.url}/api/Ingredients`,
      ingredientForm
    );
  }

  public deleteIngredient(id: number): Observable<Ingredient> {
    return this.http.delete<Ingredient>(
      `${this.backend.url}/api/Ingredients/${id}`
    );
  }

  public updateIngredient(ingredient: Ingredient) {
    return this.http.put(
      `${this.backend.url}/api/Ingredients/${ingredient.ingredientId}`,
      ingredient
    );
  }

  openManageIngredient(ingredient: Ingredient | null): void {
    if (this.dialog.openDialogs.length > 0) return;

    const dialogRef = this.dialog.open(ManageIngredientComponent, {
      data: { ingredient },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
