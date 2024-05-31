export interface Category {
  categoryId: number;
  name: string;
}

export interface CategoryForm {
  name: string;
}

export interface CategoriesState {
  categories: Category[];
  error: { error: any; statusText: string } | null;
}