import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { FullReceipt } from '../../models/receipt.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-recipe',
  standalone: true,
  imports: [RouterLink, MatButtonModule, MatIconModule, CommonModule],
  templateUrl: './recipe-card.component.html',
  styleUrl: './recipe-card.component.scss',
})
export class RecipeComponent {
  @Input() recipe: FullReceipt = {} as FullReceipt;
  @Output() onFavoriteCheck = new EventEmitter<FullReceipt>();
  @Output() onFavoriteUncheck = new EventEmitter<FullReceipt>();
  constructor() {}

  onCheckClick() {
    this.onFavoriteCheck.emit(this.recipe);
  }

  onUncheckClick() {
    this.onFavoriteUncheck.emit(this.recipe);
  }
}
