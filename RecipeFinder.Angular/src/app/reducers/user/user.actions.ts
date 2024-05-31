import { createAction, createReducer, props } from '@ngrx/store';
import {
  AuthError,
  LoginUser,
  RegisterUser,
  UserState,
} from '../../models/user.model';

export const userLogIn = createAction('[USER] log in', props<LoginUser>());
export const userLogInSuccess = createAction(
  '[USER] log in success',
  props<UserState>()
);
export const userLogInFailure = createAction(
  '[USER] log in failure',
  props<{ error: AuthError }>()
);

export const userRegister = createAction(
  '[USER] register',
  props<RegisterUser>()
);
export const userRegisterSuccess = createAction(
  '[USER] register success',
  props<UserState>()
);
export const userRegisterFailure = createAction(
  '[USER] register failure',
  props<{ error: AuthError }>()
);

export const userLogOut = createAction('[USER] log out');
export const clearUserError = createAction('[USER] clear error');
