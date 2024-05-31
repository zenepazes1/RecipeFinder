import { HttpClient } from '@angular/common/http';
import { BackendService } from './backend.service';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Receipt, ReceiptForm, ReceiptPost } from '../models/receipt.model';
import {
  ManageReceiptComponent,
  ManageReceiptData,
} from '../components/manage-receipt/manage-receipt.component';
import { MatDialog } from '@angular/material/dialog';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Store, select } from '@ngrx/store';
import { State } from '../reducers';
import { selectUserId } from '../reducers/user/user.selector';
import { DialogComponent } from '../components/dialog/dialog.component';

export type ErrorMessages = {
  [key: string]: { type: string; message: string }[];
};
export type FormErrorMessages = {
  [key: string]: string;
};
export const ERROR_MESSAGES = {
  title: [{ type: 'required', message: 'Title is required' }],
  description: [{ type: 'required', message: 'Description is required' }],
  instructions: [{ type: 'required', message: 'Instructions are required' }],
  preparationTime: [
    { type: 'required', message: 'Preparation time is required' },
    { type: 'min', message: 'Preparation time must be at least 1' },
  ],
  difficulty: [
    { type: 'required', message: 'Difficulty is required' },
    { type: 'min', message: 'Difficulty must be at least 0' },
    { type: 'max', message: 'Difficulty must be at most 5' },
  ],
  imageUrl: [{ type: 'required', message: 'Image URL is required' }],
  categoryId: [{ type: 'required', message: 'Category is required' }],
  ingredients: [
    { type: 'required', message: 'Recipe should have at least 1 ingredient' },
  ],
};

@Injectable({
  providedIn: 'root',
})
export class ReceiptsService {
  userId$: Observable<string> = this.store.pipe(select(selectUserId));
  authorId: string = '';

  constructor(
    private http: HttpClient,
    private backend: BackendService,
    private formBuilder: FormBuilder,
    private store: Store<State>,
    public dialog: MatDialog
  ) {
    this.userId$.subscribe((userId) => (this.authorId = userId));
  }

  getReceipts(): Observable<Receipt[]> {
    return this.http.get<Receipt[]>(`${this.backend.url}/api/Recipes`);
  }

  getReceipt(id: string): Observable<Receipt> {
    return this.http.get<Receipt>(`${this.backend.url}/api/Recipes/${id}`);
  }

  createReceipt(receiptPost: ReceiptPost): Observable<Receipt> {
    return this.http.post<Receipt>(
      `${this.backend.url}/api/Recipes`,
      receiptPost
    );
  }

  updateReceipt(receipt: Receipt): Observable<any> {
    const receiptPost = {
      title: receipt.title,
      description: receipt.description,
      instructions: receipt.instructions,
      preparationTime: receipt.preparationTime,
      difficulty: receipt.difficulty,
      imageUrl: receipt.imageUrl,
      authorId: receipt.authorId,
      categoryId: receipt.categoryId,
      ingredientIds: receipt.ingredients.map(
        (ingredient) => ingredient.ingredientId
      ),
    };

    return this.http.put<Receipt>(
      `${this.backend.url}/api/Recipes/${receipt.recipeId}`,
      receiptPost
    );
  }

  deleteReceipt(id: number): Observable<Receipt> {
    return this.http.delete<Receipt>(`${this.backend.url}/api/Recipes/${id}`);
  }

  openManageReceipt(receipt: Receipt | null): void {
    if (this.dialog.openDialogs.length > 0) return;

    const dialogRef = this.dialog.open(ManageReceiptComponent, {
      data: { receipt },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }

  openErrorDialog(error: any): void {
    this.dialog.open(DialogComponent, {
      data: {
        statusText: error.statusText,
        messages: [error.message],
      },
    });
  }

  createReceiptForm(data: ManageReceiptData): FormGroup<any> {
    return this.formBuilder.group({
      title: new FormControl(data.receipt?.title || '', Validators.required),
      description: new FormControl(
        data.receipt?.description || '',
        Validators.required
      ),
      instructions: new FormControl(
        data.receipt?.instructions || '',
        Validators.required
      ),
      preparationTime: new FormControl(data.receipt?.preparationTime || 1, [
        Validators.required,
        Validators.min(1),
      ]),
      difficulty: new FormControl(data.receipt?.difficulty || 1, [
        Validators.required,
        Validators.min(1),
        Validators.max(5),
      ]),
      imageUrl: new FormControl(
        data.receipt?.imageUrl || '',
        Validators.required
      ),
      categoryId: new FormControl(
        data.receipt?.categoryId || null,
        Validators.required
      ),
      authorId: new FormControl(this.authorId, Validators.required),
      ingredients: new FormControl(
        data.receipt?.ingredients || [],
        (control: AbstractControl) => {
          const ingredients = control.value;
          if (!ingredients || ingredients.length === 0) {
            return { required: true };
          }
          return null;
        }
      ),
      ingredientCtrl: new FormControl(null),
    });
  }

  mapReceiptToPost(receipt: ReceiptForm): ReceiptPost {
    return {
      title: receipt.title,
      description: receipt.description,
      instructions: receipt.instructions,
      preparationTime: receipt.preparationTime,
      difficulty: receipt.difficulty,
      imageUrl: receipt.imageUrl,
      authorId: receipt.authorId,
      categoryId: receipt.categoryId,
      ingredientIds: receipt.ingredients.map(
        (ingredient) => ingredient.ingredientId
      ),
    };
  }
}
