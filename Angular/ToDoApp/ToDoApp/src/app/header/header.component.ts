import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AddtaskButtonComponent } from '../addtask-button/addtask-button.component';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router,RouterLink } from '@angular/router';
import { DropdownComponent } from '../dropdown/dropdown.component';
import { ToastrService } from 'ngx-toastr';
import { currentComponent } from '../Enums/currentComponent';
import { Constants } from '../constants/constants';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [AddtaskButtonComponent, CommonModule, DropdownComponent, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  @Output() clickedMessage = new EventEmitter<boolean>();
  isClicked : boolean = false;
  currentUrl : string ='';
  headerText : string ='';
  constructor(private router : Router, private toastr : ToastrService){}

  ngOnInit(): void {

    const currentRoute = this.router.url;
    if (currentRoute === currentComponent.dashboard) {
      this.headerText = Constants.dashboard;
    } else if (currentRoute === currentComponent.active) {
      this.headerText = Constants.active;
    } else if (currentRoute === currentComponent.completed) {
      this.headerText = Constants.completed;
    }

    this.router.events.subscribe(event=>{
        if(event instanceof NavigationEnd){
          this.currentUrl = this.router.url;
        if(this.currentUrl===currentComponent.dashboard){
          this.headerText = Constants.dashboard;
        }
        else if(this.currentUrl===currentComponent.active){
          this.headerText = Constants.active;
        }
        else if(this.currentUrl===currentComponent.completed){
          this.headerText = Constants.completed;
        }
      }
    });
  }

  
  addTask(){
    this.isClicked = true;
    this.clickedMessage.emit(this.isClicked);
  }

  signOut(){
    this.toastr.success(Constants.signedOut);
    localStorage.removeItem('key');
    
  }

  
  
}
