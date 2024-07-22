import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { currentComponent } from '../Enums/currentComponent';
import { Constants } from '../constants/constants';

@Component({
  selector: 'app-dropdown',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './dropdown.component.html',
  styleUrl: './dropdown.component.css'
})
export class DropdownComponent implements OnInit {
  component : string ='';
  constructor(private router : Router){}

  ngOnInit(): void {
    if(this.router.url===currentComponent.dashboard){
      this.component = Constants.dashboard;
    }
    else if(this.router.url===currentComponent.active){
      this.component = Constants.active;
    }
    else{
      this.component = Constants.completed;
    }
    this.router.events.subscribe((event)=>{
      if(event instanceof NavigationEnd){
        if(this.router.url===currentComponent.dashboard){
          this.component = Constants.dashboard;
        }
        else if(this.router.url===currentComponent.active){
          this.component = Constants.active;
        }
        else{
          this.component = Constants.completed;
        }
      }
    })
  }

  onChange(event : Event){
    var selectedValue = (event.target as HTMLSelectElement).value;
    if(selectedValue === Constants.dashboard){
      this.component = Constants.dashboard;
      this.router.navigateByUrl(currentComponent.dashboard);
    }
    else if(selectedValue === Constants.active){
      this.component = Constants.active;
      this.router.navigateByUrl(currentComponent.active);
    }
    else{
      this.component = Constants.completed;
      this.router.navigateByUrl(currentComponent.completed);
    }
  }
}
