import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { LoginUser, RegisterUser, UserState } from '../../models/user.model';
import { MatDialog } from '@angular/material/dialog';
import { Store, select } from '@ngrx/store';
import { userLogIn, userRegister } from '../../reducers/user/user.actions';
import { Observable } from 'rxjs';
import { State } from '../../reducers/index';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { selectUserState } from '../../reducers/user/user.selector';
import { RegisterService } from '../../services/register.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule, MatButtonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  providers: [RegisterService],
})
export class RegisterComponent implements OnInit {
  user$: Observable<UserState> = this.store.pipe(select(selectUserState));

  loginDetails: LoginUser = {
    email: '',
    password: '',
  };

  registerDetails: RegisterUser = {
    email: '',
    password: '',
    firstName: '',
    lastName: '',
  };

  constructor(
    private store: Store<State>,
    private router: Router,
    private register: RegisterService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.user$.subscribe((user) => {
      if (user.error) {
        this.register.openDialog(user.error);
      }
      if (user.userId) {
        if (user.roles.includes('Admin')) {
          this.router.navigate(['/admin'])
        } else {
          this.router.navigate(['/profile']);
        }
      }
    });
  }

  loginSubmit(form: NgForm): void {
    if (!form.valid) return;
    this.store.dispatch(userLogIn(this.loginDetails));
  }

  registerSubmit(form: NgForm): void {
    if (!form.valid) return;
    this.store.dispatch(userRegister(this.registerDetails));
  }
}
