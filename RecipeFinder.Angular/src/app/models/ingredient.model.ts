export interface Ingredient {
  ingredientId: number;
  name: string;
}

export interface IngredientForm {
  name: string;
}

export interface IngredientsState {
  ingredients: Ingredient[];
  error: { error: any; statusText: string } | null;
}