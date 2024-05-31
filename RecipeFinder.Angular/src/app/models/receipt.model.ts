import { Category } from './category.model';
import { Ingredient } from './ingredient.model';
import { UserState } from './user.model';

export interface Receipt {
  recipeId: number;
  title: string;
  description: string;
  instructions: string;
  preparationTime: number;
  difficulty: number;
  imageUrl: string;
  authorId: string;
  author: UserState;
  categoryId: number;
  category: Category;
  ingredients: Ingredient[];
}

export interface FullReceipt extends Receipt {
  isFavorite: boolean;
}

export interface ReceiptForm {
  title: string;
  description: string;
  instructions: string;
  preparationTime: number;
  difficulty: number;
  imageUrl: string;
  authorId: string;
  categoryId: number;
  ingredients: Ingredient[];
}

export interface ReceiptPost {
  title: string;
  description: string;
  instructions: string;
  preparationTime: number;
  difficulty: number;
  imageUrl: string;
  authorId: string;
  categoryId: number;
  ingredientIds: number[];
}

export interface ReceiptsState {
  receipts: Receipt[];
  error: { error: any; statusText: string } | null;
}
