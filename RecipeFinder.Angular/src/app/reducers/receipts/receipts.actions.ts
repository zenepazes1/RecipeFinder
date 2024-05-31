import { createAction, props } from '@ngrx/store';
import { Receipt, ReceiptForm, ReceiptPost } from '../../models/receipt.model';

export const loadReceipts = createAction('[Receipts] Load Receipts');
export const loadReceiptsSuccess = createAction(
  '[Receipts] Load Receipts Success',
  props<{ receipts: Receipt[] }>()
);
export const loadReceiptsFailure = createAction(
  '[Receipts] Load Receipts Failure',
  props<{ error: any }>()
);
export const loadReceipt = createAction(
  '[Receipts] Load Receipt',
  props<{ id: string }>()
);
export const loadReceiptSuccess = createAction(
  '[Receipts] Load Receipt Success',
  props<{ receipt: Receipt }>()
);
export const loadReceiptFailure = createAction(
  '[Receipts] Load Receipt Failure',
  props<{ error: any }>()
);
export const createReceipt = createAction(
  '[Receipts] Create Receipt',
  props<{ receiptPost: ReceiptPost }>()
);
export const createReceiptSuccess = createAction(
  '[Receipts] Create Receipt Success',
  props<{ receipt: Receipt }>()
);
export const createReceiptFailure = createAction(
  '[Receipts] Create Receipt Failure',
  props<{ error: any }>()
);
export const updateReceipt = createAction(
  '[Receipts] Update Receipt',
  props<{ receipt: Receipt }>()
);
export const updateReceiptSuccess = createAction(
  '[Receipts] Update Receipt Success',
  props<{ receipt: Receipt }>()
);
export const updateReceiptFailure = createAction(
  '[Receipts] Update Receipt Failure',
  props<{ error: any }>()
);
export const deleteReceipt = createAction(
  '[Receipts] Delete Receipt',
  props<{ id: number }>()
);
export const deleteReceiptSuccess = createAction(
  '[Receipts] Delete Receipt Success',
  props<{ id: number }>()
);
export const deleteReceiptFailure = createAction(
  '[Receipts] Delete Receipt Failure',
  props<{ error: any }>()
);
