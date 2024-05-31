import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { HttpClientModule } from '@angular/common/http';
import { MenuComponent } from './components/menu/menu.component';
import { Store } from '@ngrx/store';
import { State } from './reducers';
import { selectUserId } from './reducers/user/user.selector';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    HttpClientModule,
    MenuComponent,
    CommonModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'RecipeFinder.Angular';
  isLoggedIn$: Observable<string> = this.store.select(selectUserId);

  constructor(private store: Store<State>) {}
}
