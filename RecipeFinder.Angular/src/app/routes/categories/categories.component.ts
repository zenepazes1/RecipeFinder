import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import {
  deleteCategory,
  loadCategories,
} from '../../reducers/categories/categories.actions';
import { Category } from '../../models/category.model';
import { selectCategories } from '../../reducers/categories/categories.selector';
import { Observable, Subscription, map } from 'rxjs';
import { CategoriesService } from '../../services/categories.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { selectUserId } from '../../reducers/user/user.selector';
import { RegisterService } from '../../services/register.service';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss',
})
export class CategoriesComponent implements OnDestroy {
  categories$: Observable<Category[]> = this.store.pipe(
    select(selectCategories)
  );
  isLoggedIn$: Observable<string> = this.store.pipe(select(selectUserId));
  isLoggedInSub: Subscription;
  isCategoriesEmpty$: Observable<boolean> = this.categories$.pipe(
    map((categories) => categories.length === 0)
  );

  constructor(
    private store: Store<State>,
    private categoriesService: CategoriesService,
    private registerService: RegisterService
  ) {
    this.store.dispatch(loadCategories());
    this.isLoggedInSub = this.isLoggedIn$.subscribe((userId) => {
      if (!userId) {
        this.registerService.openLoginDialog();
      }
    });
  }

  onDeleteClick(id: number) {
    this.store.dispatch(deleteCategory({ categoryId: id }));
  }

  onEditClick(category: Category) {
    this.categoriesService.openManageCategory(category);
  }

  ngOnDestroy(): void {
    this.isLoggedInSub.unsubscribe();
  }
}
