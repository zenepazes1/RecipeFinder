import { Component } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IngredientsService } from '../../services/ingredients.service';
import { CategoriesService } from '../../services/categories.service';
import { ReceiptsService } from '../../services/receipts.service';
import { selectUserRoles } from '../../reducers/user/user.selector';
import { Observable, map } from 'rxjs';
import { State } from '../../reducers';
import { Store, select } from '@ngrx/store';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [MatMenuModule, MatButtonModule, MatIconModule, CommonModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
})
export class MenuComponent {
  userRole$: Observable<string> = this.store.pipe(
    select(selectUserRoles),
    map((roles) => roles[0])
  );

  constructor(
    private ingredientsService: IngredientsService,
    private categoriesService: CategoriesService,
    private receiptsService: ReceiptsService,
    private store: Store<State>
  ) {}

  onAddIngredientClick() {
    this.ingredientsService.openManageIngredient(null);
  }

  onAddCategoryClick() {
    this.categoriesService.openManageCategory(null);
  }

  onAddReceiptClick() {
    this.receiptsService.openManageReceipt(null);
  }
}
