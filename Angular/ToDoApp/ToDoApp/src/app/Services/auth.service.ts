import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() { }
  validUser() : boolean{
    if(!!localStorage.getItem('key')){
      return true;
    }
    else{
      return false;
    }
  }
}
