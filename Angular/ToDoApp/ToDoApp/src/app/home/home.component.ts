import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { AddtaskComponent } from '../addtask/addtask.component';
import { CommonModule } from '@angular/common';
import { DropdownComponent } from '../dropdown/dropdown.component';
import { Task } from '../Interfaces/task';
import { SharedService } from '../Services/shared.service';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, SidebarComponent, AddtaskComponent, CommonModule, DropdownComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  isAddTaskClicked : boolean = false;
  task! : Task | null;
  private subscription! : Subscription;
  constructor(private sharedService:SharedService){}
  ngOnInit(): void {
    this.subscription = this.sharedService.getData().subscribe(
      task=>{
        if(task!=null){
          this.task=task;
          this.isAddTaskClicked = true;
        }
      }
    );
  }
  
  addTask($event : boolean){
    this.isAddTaskClicked = $event;
  }

  mobileAddTask($event:boolean){
    this.isAddTaskClicked = $event;
  }

  CancelClicked($event : boolean){
    this.isAddTaskClicked = !$event;
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

}
