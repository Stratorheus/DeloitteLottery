import {APP_INITIALIZER, ApplicationConfig} from '@angular/core';
import {provideRouter, Router} from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient} from "@angular/common/http";
import {ApiService} from "./core/services/api.service";
import {firstValueFrom} from "rxjs";
import {GenerationStateService} from "./core/services/generation-state.service";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    {
      provide: APP_INITIALIZER,
      useFactory: (apiService: ApiService, router: Router, stateSvc : GenerationStateService) => {
        return async () => {
          try {
            await firstValueFrom(apiService.ping());
          } catch (error) {
            console.error('API is unavailable', error);
            await router.navigate(['/error']);
          }
          stateSvc.initialize();
        };
      },
      deps: [ApiService, Router, GenerationStateService],
      multi: true,
    }
  ],
};
