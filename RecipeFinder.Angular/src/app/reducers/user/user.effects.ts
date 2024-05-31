import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { RegisterService } from '../../services/register.service';
import {
  userLogIn,
  userLogInFailure,
  userLogInSuccess,
  userRegister,
  userRegisterFailure,
  userRegisterSuccess,
} from './user.actions';
import { catchError, map, of, switchMap } from 'rxjs';
import { AuthError, UserState } from '../../models/user.model';

@Injectable()
export class UserEffects {
  constructor(
    private actions$: Actions,
    private registerService: RegisterService
  ) {}

  logInUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userLogIn),
      switchMap((action) =>
        this.registerService.loginUser(action).pipe(
          map((response: UserState) => {
            return userLogInSuccess(response);
          }),
          catchError((error: AuthError) => {
            return of(userLogInFailure({ error }));
          })
        )
      )
    )
  );

  registerUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(userRegister),
      switchMap((action) =>
        this.registerService.registerUser(action).pipe(
          map((response: UserState) => {
            return userRegisterSuccess(response);
          }),
          catchError((error: AuthError) => {
            return of(userRegisterFailure({ error }));
          })
        )
      )
    )
  );
}
