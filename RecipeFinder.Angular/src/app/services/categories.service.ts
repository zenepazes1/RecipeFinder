import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { State } from '../reducers';
import { BackendService } from './backend.service';
import { HttpClient } from '@angular/common/http';
import { Category, CategoryForm } from '../models/category.model';
import { ManageCategoryComponent } from '../components/manage-category/manage-category.component';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { AuthRequirementComponent } from '../components/auth-requirement/auth-requirement.component';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  constructor(
    private store: Store<State>,
    private http: HttpClient,
    private backend: BackendService,
    public dialog: MatDialog
  ) {}

  public getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.backend.url}/api/Categories`);
  }

  public getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.backend.url}/api/Categories/${id}`);
  }

  public addCategory(category: CategoryForm): Observable<Category> {
    return this.http.post<Category>(
      `${this.backend.url}/api/Categories`,
      category
    );
  }

  public deleteCategory(id: number) {
    return this.http.delete(`${this.backend.url}/api/Categories/${id}`);
  }

  public updateCategory(category: Category) {
    return this.http.put(
      `${this.backend.url}/api/Categories/${category.categoryId}`,
      category
    );
  }

  openManageCategory(category: Category | null): void {
    if (this.dialog.openDialogs.length > 0) return;

    const dialogRef = this.dialog.open(ManageCategoryComponent, {
      data: { category },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
