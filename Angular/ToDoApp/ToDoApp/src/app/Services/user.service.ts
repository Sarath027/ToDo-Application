import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../Interfaces/user';
import { Api } from '../constants/api';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http : HttpClient) { }
  signUp(user : User){
    return this.http.post<boolean>(Api.userEndpoint+'/SignUp', user);
  }

  signIn(user : User){
    return this.http.post(Api.userEndpoint+'/Login', user, { responseType : 'text'});
  }
}
