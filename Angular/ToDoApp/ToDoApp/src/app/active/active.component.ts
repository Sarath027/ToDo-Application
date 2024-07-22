import { Component,EventEmitter,HostListener,OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { UserTask } from '../Interfaces/user-task';
import { TaskService } from '../Services/task.service';
import { Task } from '../Interfaces/task';
import { SharedService } from '../Services/shared.service';
import { ToastrService } from 'ngx-toastr';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { currentComponent } from '../Enums/currentComponent';
import { Status } from '../Enums/status';
import { Constants } from '../constants/constants';

@Component({
  selector: 'app-active',
  standalone: true,
  imports: [CommonModule, MatProgressSpinnerModule],
  templateUrl: './active.component.html',
  styleUrl: './active.component.css'
})
export class ActiveComponent implements OnInit {
  @Output() isClicked = new EventEmitter<Task>();
  isEditClicked : boolean =false;
  tasks : UserTask[] = [];
  isTaskSelected : boolean[] = new Array(this.tasks.length).fill(false);
  currentUrl : string ='';
  currentComponentActive : boolean = false;
  currentDate : string = new Date().toUTCString();
  isCompleted : boolean = false;
  checkboxImage : string = "assets/checkbox.png";
  headerText : string ='';
  addedTime! : number;
  isLoading : boolean=false;
  isDeleteConfirmPopup : boolean=false;
  isStatusTogglePopup : boolean=false;
  taskId! : number;
  taskDescriptionContent : string ='';
  isTaskAvaliable : boolean=false;
  constructor(private router : Router, private taskService : TaskService, 
    private sharedService : SharedService,  private toastr : ToastrService) { 
    this.currentUrl = this.router.url;
    if(this.currentUrl == currentComponent.active){
      this.headerText = Status.active;
      this.isCompleted = false;
      this.currentComponentActive = true;
      this.checkboxImage = "assets/checkbox.png";
    }
    else if(this.currentUrl == currentComponent.completed){
      this.headerText = Status.completed;
      this.isCompleted = true;
      this.currentComponentActive=false;
      this.checkboxImage = "assets/checkbox-selected.png"
    }
  }

  ngOnInit(): void {
    this.isLoading=true;
    if(this.currentUrl == currentComponent.active){
      this.taskService.activeTasks().subscribe(
        tasks=>{
          if(tasks.length>0){
            this.isTaskAvaliable=true;
          }
          this.tasks = tasks;
          this.isLoading=false;
      });
    }
    else if(this.currentUrl==currentComponent.completed){
      this.tasks=[];
      this.taskService.completedTasks().subscribe(
        tasks =>{
          if(tasks.length>0){
            this.isTaskAvaliable=true;
          }
          this.tasks = tasks;
          this.isLoading=false; 
        }
      );
    }
    this.taskService.reloadObservable.subscribe(()=>{
      this.loadTasks();
    });
  }

  loadTasks(){
    this.isLoading=true;
    this.taskService.activeTasks().subscribe(
      tasks=>{
        if(tasks.length>0){
          this.isTaskAvaliable=true;
        }
        this.tasks = tasks;
        this.isLoading=false;
    }
    );
  }

  displayTaskDescription(index : number){
    this.isTaskSelected[index] = !this.isTaskSelected[index];
    for(let i:number =0;i<this.isTaskSelected.length;i++){
      if(i!=index){
        this.isTaskSelected[i]=false;
      }
    }
    var createdTime = new Date(this.tasks[index].createdOn);
    var hours: number = Math.round((new Date().getTime() - createdTime.getTime())/(1000*60*60));
    console.log(new Date().getTime());
    console.log(createdTime.getTime());
    if(hours>24){
      this.addedTime=Math.floor(hours/24);
      if(this.addedTime==1){
        this.taskDescriptionContent='day';
      }
      else{
        this.taskDescriptionContent='days';
      }
    }
    else{
      this.addedTime=hours;
      this.taskDescriptionContent='hours';
    }
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const clickedInside = (event.target as HTMLElement).closest('.task, .task-description-container');
    if (!clickedInside) {
      this.isTaskSelected.fill(false);
    }
  }

  completeTask(taskId : number){
    this.isStatusTogglePopup=true;
    this.sharedService.setTaskIdData(taskId);
  }

  toggleConfirm(){
    this.isLoading=true;
    this.sharedService.getTaskId().subscribe(
      taskId =>{
        this.taskId=taskId;
      }
    );
    return this.taskService.activeToCompleteTask(this.taskId).subscribe(
      tasks=>{
        if(tasks.length>0){
          this.isTaskAvaliable=true;
        }
        this.tasks=tasks;
        this.isLoading=false;
        if(this.isCompleted){
          this.toastr.success(Constants.taskActive);
        }
        else{
          this.toastr.success(Constants.taskComplete);
        }
        this.isStatusTogglePopup=false;
      }
    );
  }

  cancelToggle(){
    this.isStatusTogglePopup=false;
  }

  editTask(taskid : number, taskTitle : string, taskDescription : string){
    var task:Task = {
      taskId : taskid,
      taskName : taskTitle,
      description : taskDescription
    }
    this.sharedService.setData(task);
  }

  deleteTask(taskId : number){
    this.isDeleteConfirmPopup=true;
    this.sharedService.setTaskIdData(taskId); 
  }

  deleteConfirm(){
    this.sharedService.getTaskId().subscribe(
      taskId =>{
        this.taskId=taskId;
      }
    );
    this.isLoading=true;
    return this.taskService.deleteTask(this.taskId).subscribe(
      tasks=>{
        if(tasks.length>0){
          this.isTaskAvaliable=true;
        }
        this.tasks=tasks;
        this.isLoading=false;
        this.toastr.success(Constants.taskDeleted);
        this.isDeleteConfirmPopup=false;
        this.isLoading=false;
      }
    );
  }

  cancelDelete(){
    this.isDeleteConfirmPopup=false;
  }



}
