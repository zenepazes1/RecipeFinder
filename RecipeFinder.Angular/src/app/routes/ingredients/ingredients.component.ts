import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import {
  deleteIngredient,
  loadIngredients,
} from '../../reducers/ingredients/ingredients.actions';
import { Observable, Subscription } from 'rxjs';
import { Ingredient } from '../../models/ingredient.model';
import { selectIngredients } from '../../reducers/ingredients/ingredients.selector';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { IngredientsService } from '../../services/ingredients.service';
import { selectUserId } from '../../reducers/user/user.selector';
import { RegisterService } from '../../services/register.service';

@Component({
  selector: 'app-ingredients',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule],
  templateUrl: './ingredients.component.html',
  styleUrl: './ingredients.component.scss',
  providers: [IngredientsService],
})
export class IngredientsComponent implements OnDestroy {
  ingredients$: Observable<Ingredient[]> = this.store.pipe(
    select(selectIngredients)
  );
  isLoggedIn$: Observable<string> = this.store.pipe(select(selectUserId));
  isLoggedInSub: Subscription;

  constructor(
    private store: Store<State>,
    private ingredientsService: IngredientsService,
    private registerService: RegisterService
  ) {
    this.store.dispatch(loadIngredients());

    this.isLoggedInSub = this.isLoggedIn$.subscribe((userId) => {
      if (!userId) {
        this.registerService.openLoginDialog();
      }
    });
  }

  onDeleteClick(id: number) {
    this.store.dispatch(deleteIngredient({ ingredientId: id }));
  }

  onEditClick(ingredient: Ingredient) {
    this.ingredientsService.openManageIngredient(ingredient);
  }

  ngOnDestroy(): void {
    this.isLoggedInSub.unsubscribe();
  }
}
