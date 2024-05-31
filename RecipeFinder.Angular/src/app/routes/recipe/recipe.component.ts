import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable, Subscription, map, switchMap } from 'rxjs';
import { State } from '../../reducers';
import { selectReceipt } from '../../reducers/receipts/receipts.selector';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { Receipt } from '../../models/receipt.model';
import { loadReceipts } from '../../reducers/receipts/receipts.actions';

@Component({
  selector: 'app-recipe',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatChipsModule],
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.scss',
})
export class RecipeComponent {
  recipe$: Observable<Receipt | undefined>;

  constructor(private route: ActivatedRoute, private store: Store<State>) {
    this.store.dispatch(loadReceipts());

    this.recipe$ = this.route.params.pipe(
      map((params) => {
        const id = +params['id'];
        return id;
      }),
      switchMap((id) =>
        this.store.pipe(select(selectReceipt({ recipeId: id })))
      )
    );
  }
}
