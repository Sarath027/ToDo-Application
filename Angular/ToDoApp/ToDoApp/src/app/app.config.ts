import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideToastr } from 'ngx-toastr';
import { AuthInterceptor } from './Interceptor/auth.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideHttpClient(withInterceptors([AuthInterceptor])), 
  provideAnimations(),provideToastr({timeOut:2000,preventDuplicates:true}), provideAnimationsAsync()]
};
