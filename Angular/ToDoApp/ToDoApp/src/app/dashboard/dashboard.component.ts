import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskStatusComponent } from '../task-status/task-status.component';
import { TaskService } from '../Services/task.service';
import { UserTask } from '../Interfaces/user-task';;
import { ToastrService } from 'ngx-toastr';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Status } from '../Enums/status';
import { Constants } from '../constants/constants';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, TaskStatusComponent, MatProgressSpinnerModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {

  currentDate : string = new Date().toUTCString();
  allTasks : UserTask[]=[];
  isTaskSelected:boolean[] = [];
  addedTime! : number;
  isLoading : boolean=false;
  isConfirmPopup:boolean=false;
  isTaskAvaliable : boolean = false;
  taskStatus : number[]=[0,0];
  isDeleteClicked : boolean=false;
  isSignoutDisable:boolean=false;
  constructor(private taskService : TaskService, private toastr : ToastrService) { }
  
  ngOnInit(): void {
    this.isLoading=true;
    this.taskService.getAllTasks().subscribe(
      tasks=>{
        this.allTasks = tasks.sort((a, b) => a.status.localeCompare(b.status));
        if(tasks.length>0){
          this.isTaskAvaliable=true;
        }
        this.isLoading=false;
        this.isTaskSelected  = new Array(this.allTasks.length).fill(false);
        for(let i:number =0;i<this.allTasks.length;i++){
          if(this.allTasks[i].status===Status.completed){
            this.isTaskSelected[i]=true;
          }
          else{
            this.isTaskSelected[i]=false;
          }
        }
        this.taskService.reloadObservable.subscribe(()=>{
          this.loadTasks();
        });
      }
    );
  }

  loadTasks(){
    this.isLoading=true;
    this.taskService.getAllTasks().subscribe(
      tasks => {
        this.isLoading=false;
        if(tasks.length>0){
          this.isTaskAvaliable=true;
        }
        this.isTaskSelected.splice(0,0,false);
        this.allTasks = tasks.sort((a, b) => a.status.localeCompare(b.status));
      }
    );
  }

  deleteAllTasks(){
    this.isConfirmPopup=true;
  }

  deleteConfirm(){
    this.isLoading=true;
    this.isDeleteClicked=true;
    this.taskService.deleteAllTask().subscribe(
      tasks=>{
      this.allTasks=tasks;
      this.toastr.success(Constants.allTasksDeleted);
      this.isTaskAvaliable=false;
      this.isLoading=false;
    }
    );
    this.isConfirmPopup=false;
  }

  cancelDelete(){
    this.isConfirmPopup=false;
  }
}
