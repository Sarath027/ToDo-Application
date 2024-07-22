import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { currentComponent } from '../Enums/currentComponent';

export const authGuard: CanActivateFn = (route, state) => {
   var authService : AuthService = inject(AuthService);
   var router : Router = inject(Router);
   var toastr : ToastrService = inject(ToastrService);
   if(authService.validUser()){
    return true;
   }
   else{
    router.navigateByUrl(currentComponent.login);
    toastr.error("Please Login to continue");
    return false;
   }
};
