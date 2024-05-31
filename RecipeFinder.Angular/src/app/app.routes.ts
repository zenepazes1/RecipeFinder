import { Routes } from '@angular/router';
import { HomeComponent } from './routes/home/home.component';
import { RegisterComponent } from './routes/register/register.component';
import { ProfileComponent } from './routes/profile/profile.component';
import { IngredientsComponent } from './routes/ingredients/ingredients.component';
import { RecipesComponent } from './routes/recipes/recipes.component';
import { CategoriesComponent } from './routes/categories/categories.component';
import { AuthGuardService } from './services/authguard.service';
import { FavoriteComponent } from './routes/favorite/favorite.component';
import { RecipeComponent } from './routes/recipe/recipe.component';
import { AdminComponent } from './routes/admin/admin.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuardService],
  },
  { path: 'ingredients', component: IngredientsComponent },
  { path: 'recipes', component: RecipesComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'favorite', component: FavoriteComponent },
  { path: 'recipes/:id', component: RecipeComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuardService] },
];
