import { COMMA, ENTER } from '@angular/cdk/keycodes';
import {
  Component,
  ElementRef,
  Inject,
  OnDestroy,
  ViewChild,
  inject,
} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
  MatDialog,
} from '@angular/material/dialog';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Receipt, ReceiptForm, ReceiptPost } from '../../models/receipt.model';
import { Store, select } from '@ngrx/store';
import { State } from '../../reducers';
import {
  Observable,
  Subscription,
  combineLatest,
  map,
  merge,
  of,
  startWith,
  switchMap,
} from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { selectUserId } from '../../reducers/user/user.selector';
import { MatIconModule } from '@angular/material/icon';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { CommonModule, NgStyle } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { selectCategories } from '../../reducers/categories/categories.selector';
import { Category } from '../../models/category.model';
import { loadCategories } from '../../reducers/categories/categories.actions';
import { loadIngredients } from '../../reducers/ingredients/ingredients.actions';
import { selectIngredients } from '../../reducers/ingredients/ingredients.selector';
import { Ingredient } from '../../models/ingredient.model';
import {
  MatAutocompleteSelectedEvent,
  MatAutocompleteModule,
} from '@angular/material/autocomplete';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {
  createReceipt,
  updateReceipt,
} from '../../reducers/receipts/receipts.actions';
import { selectReceiptsError } from '../../reducers/receipts/receipts.selector';
import { DialogComponent } from '../dialog/dialog.component';
import {
  ERROR_MESSAGES,
  ErrorMessages,
  FormErrorMessages,
  ReceiptsService,
} from '../../services/receipts.service';
import { CloudinaryService } from '../../services/cloudinary.service';
import { Router } from '@angular/router';

export interface ManageReceiptData {
  receipt: Receipt;
}

@Component({
  selector: 'app-manage-receipt',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    NgStyle,
    MatSelectModule,
    CommonModule,
    MatChipsModule,
    MatAutocompleteModule,
  ],
  templateUrl: './manage-receipt.component.html',
  styleUrl: './manage-receipt.component.scss',
})
export class ManageReceiptComponent implements OnDestroy {
  userId$: Observable<string> = this.store.pipe(select(selectUserId));
  categories$: Observable<Category[]> = this.store.pipe(
    select(selectCategories)
  );
  ingredients$: Observable<Ingredient[]> = this.store.pipe(
    select(selectIngredients)
  );
  unselectedIngredients$ = this.ingredients$.pipe(
    map((ingredients) =>
      ingredients.filter(
        (ingredient) =>
          !this.recipeForm
            .get('ingredients')
            ?.value.reduce(
              (acc: boolean, curr: Ingredient) =>
                acc || curr.ingredientId === ingredient.ingredientId,
              false
            )
      )
    )
  );
  receiptsErrors$: Observable<any> = this.store.pipe(
    select(selectReceiptsError)
  );

  recipeForm: FormGroup = this.receiptsService.createReceiptForm(this.data);

  separatorKeysCodes: number[] = [ENTER, COMMA];
  filteredIngredients$: Observable<Ingredient[]>;

  @ViewChild('ingredientInput') ingredientInput!: ElementRef<HTMLInputElement>;
  announcer = inject(LiveAnnouncer);

  imageFile: File | null = null;

  recipeErrorsSub: Subscription;

  constructor(
    public manageReceiptRef: MatDialogRef<ManageReceiptComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ManageReceiptData,
    private store: Store<State>,
    private receiptsService: ReceiptsService,
    private cloudinaryService: CloudinaryService,
    private router: Router,
    public dialog: MatDialog
  ) {
    this.store.dispatch(loadCategories());
    this.store.dispatch(loadIngredients());

    merge(this.recipeForm.statusChanges, this.recipeForm.valueChanges)
      .pipe(takeUntilDestroyed())
      .subscribe(() => this.updateErrorMessages());

    const ingredientInputChanges$ = this.recipeForm
      .get('ingredientCtrl')!
      .valueChanges.pipe(startWith(null));

    const ingredientsArrayChanges$ = this.recipeForm
      .get('ingredients')!
      .valueChanges.pipe(startWith(this.recipeForm.get('ingredients')?.value));

    this.filteredIngredients$ = combineLatest([
      ingredientInputChanges$,
      ingredientsArrayChanges$,
    ]).pipe(
      switchMap(([ingredientName, ingredients]) =>
        ingredientName
          ? this._filter(ingredientName)
          : this.unselectedIngredients$
      )
    );

    this.recipeErrorsSub = this.receiptsErrors$.subscribe((error) => {
      if (error) {
        this.receiptsService.openErrorDialog(error);
      }
    });
  }

  private _filter(value: string): Observable<Ingredient[]> {
    if (typeof value !== 'string') return this.unselectedIngredients$;

    const filterValue = value.toLowerCase();
    return this.unselectedIngredients$.pipe(
      map((ingredients) =>
        ingredients.filter((ingredient) =>
          ingredient.name.toLowerCase().includes(filterValue)
        )
      )
    );
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    const ingredients: Ingredient[] = this.recipeForm.get('ingredients')?.value;
    const selectedIngredientId = event.option.value;
    const selectedIngredient$ = this.ingredients$.pipe(
      map((ingredients) =>
        ingredients.find(
          (ingredient) => ingredient.ingredientId === selectedIngredientId
        )
      )
    );
    selectedIngredient$.subscribe((selectedIngredient) => {
      this.recipeForm
        .get('ingredients')
        ?.setValue([...ingredients, selectedIngredient]);
    });
    this.ingredientInput.nativeElement.value = '';
    this.ingredientInput.nativeElement.blur();
    this.recipeForm.get('ingredientCtrl')?.setValue(null);
  }

  errorMessages: ErrorMessages = ERROR_MESSAGES;

  formErrorMessages: FormErrorMessages = {
    title: '',
    description: '',
    instructions: '',
    preparationTime: '',
    difficulty: '',
    imageUrl: '',
    category: '',
    ingredients: '',
  };

  updateErrorMessages(): void {
    for (const field in this.errorMessages) {
      const control = this.recipeForm.get(field);
      if (control && (control.dirty || control.touched) && control.invalid) {
        const messages = this.errorMessages[field];
        for (const key in control.errors) {
          this.formErrorMessages[field] =
            messages.find((m) => m.type === key)?.message ?? '';
        }
      }
    }
  }
  updateAllErrorMessages(): void {
    console.log(this.recipeForm);
    for (const field in this.errorMessages) {
      const control = this.recipeForm.get(field);
      if (control && control.invalid) {
        control.markAsTouched();
        const messages = this.errorMessages[field];
        for (const key in control.errors) {
          this.formErrorMessages[field] =
            messages.find((m) => m.type === key)?.message ?? '';
        }
      }
    }
  }

  setFileData(event: Event): void {
    const eventTarget: HTMLInputElement | null =
      event.target as HTMLInputElement | null;
    if (eventTarget?.files?.[0]) {
      const file: File = eventTarget.files[0];
      this.imageFile = file;
      const reader = new FileReader();
      reader.addEventListener('load', () => {
        this.recipeForm.get('imageUrl')?.setValue(reader.result as string);
      });
      console.log(file);
      reader.readAsDataURL(file);
    }
  }

  onCancelClick(): void {
    this.manageReceiptRef.close();
  }

  addIngredient(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      const ingredients: Ingredient[] =
        this.recipeForm.get('ingredients')?.value;

      this.ingredients$.subscribe((ingredient) => {
        const selectedIngredient = ingredient.find(
          (ingredient) => ingredient.name === value
        );
        if (selectedIngredient) {
          this.recipeForm
            .get('ingredients')
            ?.setValue([...ingredients, selectedIngredient]);
        }
      });
    }

    event.chipInput!.clear();
    this.recipeForm.get('ingredientCtrl')?.setValue(null);
  }

  removeIngredient(ingredient: Ingredient): void {
    const ingredients: Ingredient[] = this.recipeForm.get('ingredients')?.value;
    this.recipeForm
      .get('ingredients')
      ?.setValue(
        ingredients.filter((i) => i.ingredientId !== ingredient.ingredientId)
      );
  }

  async receiptManageSubmit(ingredientForm: FormGroup): Promise<void> {
    if (!this.recipeForm.valid) {
      this.updateAllErrorMessages();
      return;
    }
    const receiptDetails: ReceiptForm = this.recipeForm.value;
    let cloudinaryImageUrl: string = '';

    if (!this.data.receipt?.imageUrl) {
      if (!this.imageFile) {
        this.receiptsService.openErrorDialog({
          statusText: 'Error',
          message: 'Please select an image',
        });
        return;
      } else {
        const imageFile: File = this.imageFile;
        cloudinaryImageUrl = await this.cloudinaryService.uploadImage(
          imageFile
        );
      }
    } else {
      if (!this.imageFile) {
        cloudinaryImageUrl = this.data.receipt.imageUrl;
      } else {
        const imageFile: File = this.imageFile;
        cloudinaryImageUrl = await this.cloudinaryService.uploadImage(
          imageFile
        );
      }
    }

    receiptDetails.imageUrl = cloudinaryImageUrl;

    const receiptPost: ReceiptPost =
      this.receiptsService.mapReceiptToPost(receiptDetails);

    if (this.data.receipt) {
      const newReceipt: Receipt = {
        ...this.data.receipt,
        ...receiptDetails,
      };
      this.store.dispatch(updateReceipt({ receipt: newReceipt }));
    } else {
      this.store.dispatch(createReceipt({ receiptPost: receiptPost }));
    }
    this.onCancelClick();
    this.router.navigate(['/profile']);
  }

  ngOnDestroy(): void {
    this.recipeErrorsSub.unsubscribe();
  }
}
