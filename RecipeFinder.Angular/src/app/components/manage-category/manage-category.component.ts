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
import { Store } from '@ngrx/store';
import { Category, CategoryForm } from '../../models/category.model';
import { State } from '../../reducers';
import { addCategory, updateCategory } from '../../reducers/categories/categories.actions';

export interface ManageCategoryData {
  category: Category | null;
}

@Component({
  selector: 'app-manage-category',
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
  templateUrl: './manage-category.component.html',
  styleUrl: './manage-category.component.scss'
})
export class ManageCategoryComponent {
  constructor(
    public manageCategoryRef: MatDialogRef<ManageCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ManageCategoryData,
    private store: Store<State>
  ) {}

  categoryDetails: CategoryForm = {
    name: this.data.category?.name || '',
  };

  onCancelClick(): void {
    this.manageCategoryRef.close();
  }

  categoryManageSubmit(ingredientForm: NgForm): void {
    if (!ingredientForm.valid) return;
    if (this.data.category) {
      const newCategory: Category = {
        ...this.data.category,
        ...this.categoryDetails,
      };
      this.store.dispatch(updateCategory({ category: newCategory }));
    } else {
      this.store.dispatch(
        addCategory({ categoryForm: this.categoryDetails })
      );
    }
    this.onCancelClick();
  }
}
