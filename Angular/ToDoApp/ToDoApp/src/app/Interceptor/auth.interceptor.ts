import {HttpRequest, HttpEvent, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';
import { currentComponent } from '../Enums/currentComponent';

export function AuthInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  var router : Router = inject(Router);
  var authToken = localStorage.getItem('key');
  var newReq = req.clone({setHeaders: {
                              Authorization: `Bearer ${authToken}`
                          }});
  return next(newReq).pipe(
    catchError((err:any)=>{
      if(err instanceof HttpErrorResponse){
        if(err.status === 401){
          localStorage.removeItem('key');
          router.navigateByUrl(currentComponent.login);
        }
      }
      return throwError(()=>err);
    })
  );
}