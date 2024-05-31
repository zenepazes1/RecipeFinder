import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginUser, RegisterUser } from '../models/user.model';
import { Observable } from 'rxjs';
import { BackendService } from './backend.service';
import { DialogComponent } from '../components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AuthRequirementComponent } from '../components/auth-requirement/auth-requirement.component';
import { Store } from '@ngrx/store';
import { State } from '../reducers';
import { clearUserError } from '../reducers/user/user.actions';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor(
    private http: HttpClient,
    private backend: BackendService,
    private store: Store<State>,
    public dialog: MatDialog
  ) {}

  public loginUser(user: LoginUser): Observable<any> {
    return this.http.post(`${this.backend.url}/api/Auth/login`, user);
  }

  public registerUser(user: RegisterUser): Observable<any> {
    return this.http.post(`${this.backend.url}/api/Auth/register`, user);
  }

  openDialog(error: any): void {
    const messages = Array.isArray(error.error)
      ? this.makeMessages(error.error)
      : [error.error];

    if (this.dialog.openDialogs.length > 0) return;

    const dialogRef = this.dialog.open(DialogComponent, {
      data: { statusText: error.statusText, messages: messages },
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.store.dispatch(clearUserError());
    });
  }

  makeMessages(error: [{ code: string; description: string }]): string[] {
    return error.map((e) => e.description);
  }

  openLoginDialog(): void {
    if (this.dialog.openDialogs.length > 0) return;

    const dialogRef = this.dialog.open(AuthRequirementComponent);

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
