import { createReducer, on } from '@ngrx/store';
import { UserState } from '../../models/user.model';
import {
  clearUserError,
  userLogIn,
  userLogInFailure,
  userLogInSuccess,
  userLogOut,
  userRegister,
  userRegisterFailure,
  userRegisterSuccess,
} from './user.actions';

export const initialState: UserState = {
  userId: '',
  token: '',
  email: '',
  firstName: '',
  lastName: '',
  roles: ['User'],
  error: null,
};

export const userReducer = createReducer(
  initialState,
  on(userLogIn, (state, { email, password }) => {
    return {
      ...state,
      email,
      password,
      error: null,
    };
  }),
  on(
    userLogInSuccess,
    (state, { userId, token, email, firstName, lastName, roles }) => {
      return {
        ...state,
        userId,
        token,
        email,
        firstName,
        lastName,
        roles,
        error: null,
      };
    }
  ),
  on(userLogInFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(userRegister, (state, { email, password, firstName, lastName }) => {
    return {
      ...state,
      email,
      password,
      firstName,
      lastName,
      error: null,
    };
  }),
  on(userRegisterSuccess, (state, { userId, email, firstName, lastName }) => {
    return {
      ...state,
      userId,
      email,
      firstName,
      lastName,
      error: null,
    };
  }),
  on(userRegisterFailure, (state, { error }) => {
    return {
      ...state,
      error,
    };
  }),
  on(userLogOut, (state) => {
    return {
      ...initialState,
      error: null,
    };
  }),
  on(clearUserError, (state) => {
    return {
      ...state,
      error: null,
    };
  })
);
