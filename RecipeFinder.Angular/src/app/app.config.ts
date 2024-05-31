import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { provideEffects } from '@ngrx/effects';
import { provideHttpClient } from '@angular/common/http';
import { AuthGuardService } from './services/authguard.service';
import { BackendService } from './services/backend.service';
import { effects, reducers } from './reducers';
import { CloudinaryService } from './services/cloudinary.service';
import { FavoriteRecipesService } from './services/favorite-recipes.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimationsAsync(),
    provideStore(reducers),
    provideEffects(...effects),
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
    AuthGuardService,
    BackendService,
    CloudinaryService,
    FavoriteRecipesService,
  ],
};
