import { createFeatureSelector, createSelector } from '@ngrx/store';
import { UserState } from '../../models/user.model';

export const selectUserState = createFeatureSelector<UserState>('user');

export const selectUserId = createSelector(selectUserState, (user: UserState) => user.userId);

export const selectUserRoles = createSelector(selectUserState, (user: UserState) => user.roles);