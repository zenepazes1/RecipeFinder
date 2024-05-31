import { createReducer, on } from "@ngrx/store";
import { ReceiptsState } from "../../models/receipt.model";
import { createReceiptFailure, createReceiptSuccess, deleteReceiptFailure, deleteReceiptSuccess, loadReceiptFailure, loadReceiptSuccess, loadReceiptsFailure, loadReceiptsSuccess, updateReceiptFailure, updateReceiptSuccess } from "./receipts.actions";

export const initialState: ReceiptsState = {
  receipts: [],
  error: null,
};

export const receiptsReducer = createReducer(
  initialState,
  on(loadReceiptsSuccess, (state, { receipts }) => {
    return {
      ...state,
      receipts,
      error: null,
    };
  }),
  on(loadReceiptsFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(loadReceiptSuccess, (state, { receipt }) => {
    return {
      ...state,
      receipts: [...state.receipts, receipt],
      error: null,
    };
  }),
  on(loadReceiptFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(createReceiptSuccess, (state, { receipt }) => {
    return {
      ...state,
      receipts: [...state.receipts, receipt],
      error: null,
    };
  }),
  on(createReceiptFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(updateReceiptSuccess, (state, { receipt }) => {
    return {
      ...state,
      receipts: state.receipts.map((existingReceipt) => {
        return existingReceipt.recipeId === receipt.recipeId ? receipt : existingReceipt;
      }),
      error: null,
    };
  }),
  on(updateReceiptFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(deleteReceiptSuccess, (state, { id }) => {
    return {
      ...state,
      receipts: state.receipts.filter((receipt) => receipt.recipeId !== id),
      error: null,
    };
  }),
  on(deleteReceiptFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  })
);