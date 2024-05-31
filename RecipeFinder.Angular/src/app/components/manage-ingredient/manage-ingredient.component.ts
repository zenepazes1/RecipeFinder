import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, NgForm } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Ingredient, IngredientForm } from '../../models/ingredient.model';
import { Store } from '@ngrx/store';
import { State } from '../../reducers';
import {
  addIngredient,
  updateIngredient,
} from '../../reducers/ingredients/ingredients.actions';

export interface ManageIngredientData {
  ingredient: Ingredient | null;
}

@Component({
  selector: 'app-manage-ingredient',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
  ],
  templateUrl: './manage-ingredient.component.html',
  styleUrl: './manage-ingredient.component.scss',
})
export class ManageIngredientComponent {
  constructor(
    public manageIngredientRef: MatDialogRef<ManageIngredientComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ManageIngredientData,
    private store: Store<State>
  ) {}

  ingredientDetails: IngredientForm = {
    name: this.data.ingredient?.name || '',
  };

  onCancelClick(): void {
    this.manageIngredientRef.close();
  }

  ingredientManageSubmit(ingredientForm: NgForm): void {
    if (!ingredientForm.valid) return;
    if (this.data.ingredient) {
      const newIngredient: Ingredient = {
        ...this.data.ingredient,
        ...this.ingredientDetails,
      };
      this.store.dispatch(updateIngredient({ ingredient: newIngredient }));
    } else {
      this.store.dispatch(
        addIngredient({ ingredientForm: this.ingredientDetails })
      );
    }
    this.onCancelClick();
  }
}
