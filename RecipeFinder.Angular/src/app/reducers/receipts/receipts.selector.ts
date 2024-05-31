import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Receipt, ReceiptsState } from '../../models/receipt.model';

export const selectReceiptsState =
  createFeatureSelector<ReceiptsState>('receipts');
export const selectReceipts = createSelector(
  selectReceiptsState,
  (receiptsState: ReceiptsState) => receiptsState.receipts
);

export const selectReceipt = (props: { recipeId: number }) =>
  createSelector(selectReceipts, (receipts) => {
    const receipt = receipts.find((receipt) => {
      const state = receipt.recipeId === props.recipeId;
      console.log(
        `receipt.recipeId: ${receipt.recipeId} === props.recipeId: ${props.recipeId} = ${state}`
      );
      return state;
    });
    console.log(receipt);
    return receipt;
  });

export const selectReceiptsError = createSelector(
  selectReceiptsState,
  (receiptsState: ReceiptsState) => receiptsState.error
);

export const selectUserReceipts = (props: { userId: string }) =>
  createSelector(selectReceipts, (receipts) => {
    return receipts.filter((receipt) => receipt.authorId === props.userId);
  });
