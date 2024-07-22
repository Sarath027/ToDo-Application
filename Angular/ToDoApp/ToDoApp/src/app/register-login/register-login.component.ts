import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormControl, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { User } from '../Interfaces/user';
import { UserService } from '../Services/user.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../Services/auth.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { currentComponent } from '../Enums/currentComponent';
import { Constants } from '../constants/constants';

@Component({
  selector: 'app-register-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatProgressSpinnerModule],
  templateUrl: './register-login.component.html',
  styleUrl: './register-login.component.css'
})
export class RegisterLoginComponent implements OnInit {
  loginPage : boolean = true;
  signupPage : boolean = false;
  buttonContent : string = Constants.signIn;
  footerContent : string = Constants.newAccount;
  isFormSubmitted : boolean = false;
  currentUrl : string ='';
  token : string = '';
  isLoading : boolean=false;
  isExistUser : boolean=false;
  passwordHide:boolean=true;
  constructor(private router : Router, private userService : UserService, private toastr:ToastrService,
    private authService : AuthService){
    this.currentUrl = router.url;
  }

  ngOnInit(): void {
    if(this.authService.validUser()){
      this.router.navigateByUrl(currentComponent.dashboard);
    }
    if(this.currentUrl == currentComponent.signup){
      this.buttonContent = Constants.signUp;
      this.footerContent = Constants.accountExist;
      this.loginPage = !this.loginPage;
      this.signupPage = !this.signupPage;
    }
  }

  toggleLoginSignup(){
    if(this.currentUrl === currentComponent.login){
      this.router.navigateByUrl(currentComponent.signup);
    }
    else if(this.currentUrl === currentComponent.signup){
      this.router.navigateByUrl(currentComponent.login);;
    }
    this.isFormSubmitted = !this.isFormSubmitted;
    this.userDetails.reset();
  }

  userDetails = new FormGroup({
    username : new FormControl('',[
      Validators.required,
      Validators.pattern(/^(?!\s*$).+/)
    ]),
    password : new FormControl('',[
      Validators.required,
      Validators.minLength(8)
    ])
  });

  onSubmit(){
    this.isFormSubmitted=true;
    var user : User ={
      userName : this.userDetails.value.username,
      password : this.userDetails.value.password
    }
    if(this.loginPage){
      if(user.userName!="" && user.password!=""){
        this.isLoading=true;
      }
      this.userService.signIn(user).subscribe({
        next:token =>{
          this.token=token;
          localStorage.setItem('key', token);
          setTimeout(() => {
            this.isLoading=false;
            if(this.authService.validUser()){
              this.router.navigateByUrl(currentComponent.dashboard);
              this.toastr.success(Constants.loginSuccess);
            }
          }, 1500);
        },
        error :error =>{
          setTimeout(() => {
            this.isLoading=false;
            if(error.status === 400){
              console.log(error.error.message);
              this.toastr.error(Constants.invalidDetails);
            }
            else{
              this.toastr.error(Constants.error);
            }
          }, 1500);
        }
    });
    }
    else if(this.userDetails.valid && this.signupPage){
      this.isFormSubmitted = true;
      this.isLoading=true;
      this.userService.signUp(user).subscribe({
        next: userExist=> {
          this.isExistUser=userExist;
        if(this.isExistUser){
          setTimeout(() => {
            this.isLoading=false;
            this.toastr.error(Constants.userExists);
          }, 1500); 
        }
        else{
          setTimeout(() => {
            this.isLoading=false;
            this.toastr.success(Constants.signupSuccess);
            this.router.navigateByUrl(currentComponent.login);
          }, 1500);
        }
        },
      });
    }
  }

  passwordToggle(){
    var password = document.getElementsByClassName('password')[0];
    var type = password.getAttribute('type')==='password'?'text':'password';
    this.passwordHide=!this.passwordHide;
    password.setAttribute("type", type);
  }
}