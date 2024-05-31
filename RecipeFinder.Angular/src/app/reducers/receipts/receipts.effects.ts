import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import {
  createReceipt,
  createReceiptFailure,
  createReceiptSuccess,
  deleteReceipt,
  deleteReceiptFailure,
  deleteReceiptSuccess,
  loadReceipt,
  loadReceiptFailure,
  loadReceiptSuccess,
  loadReceipts,
  loadReceiptsFailure,
  loadReceiptsSuccess,
  updateReceipt,
  updateReceiptFailure,
  updateReceiptSuccess,
} from './receipts.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { ReceiptsService } from '../../services/receipts.service';
import { Receipt } from '../../models/receipt.model';

@Injectable()
export class ReceiptsEffects {
  constructor(
    private actions$: Actions,
    private receiptsService: ReceiptsService
  ) {}

  loadReceipts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadReceipts),
      switchMap(() =>
        this.receiptsService.getReceipts().pipe(
          map((response: Receipt[]) => {
            return loadReceiptsSuccess({ receipts: response });
          }),
          catchError((error: any) => {
            return of(loadReceiptsFailure({ error }));
          })
        )
      )
    )
  );

  loadReceipt$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadReceipt),
      switchMap((action) =>
        this.receiptsService.getReceipt(action.id).pipe(
          map((response: Receipt) => {
            return loadReceiptSuccess({ receipt: response });
          }),
          catchError((error: any) => {
            return of(loadReceiptFailure({ error }));
          })
        )
      )
    )
  );

  createReceipt$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createReceipt),
      switchMap((action) =>
        this.receiptsService.createReceipt(action.receiptPost).pipe(
          map((response: Receipt) => {
            return createReceiptSuccess({ receipt: response });
          }),
          catchError((error: any) => {
            return of(createReceiptFailure({ error }));
          })
        )
      )
    )
  );

  updateReceipt$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateReceipt),
      switchMap((action) =>
        this.receiptsService.updateReceipt(action.receipt).pipe(
          map((response: any) => {
            return updateReceiptSuccess({ receipt: action.receipt });
          }),
          catchError((error: any) => {
            return of(updateReceiptFailure({ error }));
          })
        )
      )
    )
  );

  deleteReceipt$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteReceipt),
      switchMap((action) =>
        this.receiptsService.deleteReceipt(action.id).pipe(
          map(() => {
            return deleteReceiptSuccess({ id: action.id });
          }),
          catchError((error: any) => {
            return of(deleteReceiptFailure({ error }));
          })
        )
      )
    )
  );
}
