import { Component, EventEmitter, Output,OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Task } from '../Interfaces/task';
import { TaskService } from '../Services/task.service';
import { SharedService } from '../Services/shared.service';
import { ToastrService } from 'ngx-toastr';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';
import { Constants } from '../constants/constants';

@Component({
  selector: 'app-addtask',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule, MatProgressSpinnerModule, CommonModule],
  templateUrl: './addtask.component.html',
  styleUrl: './addtask.component.css'
})
export class AddtaskComponent implements OnInit{
  @Output() isClicked = new EventEmitter<boolean>();
  isCancelClicked : boolean = false;
  isSaveClicked : boolean = false;
  editTask! : Task | null;
  isEditTask : boolean = false;
  isLoading : boolean=false;
  isFormSubmitted : boolean = false;
  constructor(private taskService : TaskService, private router : Router, 
    private sharedService : SharedService,  private toastr : ToastrService){}

  ngOnInit(): void {
    this.sharedService.getData().subscribe(
      task=>{this.editTask=task;
        if(task!=null){
          this.isFormSubmitted=true;
          this.isEditTask=true;
          this.taskDetails.patchValue({
            title : task.taskName,
            description : task.description
          });
        }
        
      }
    )
  }

  taskDetails = new FormGroup({
    title : new FormControl('',[
      Validators.required,
      Validators.pattern(/^(?!\s*$).+/)
    ]),
    description : new FormControl('',[
      Validators.required,
    ])
  });

  addTask(){
    this.isFormSubmitted=true;
    if(this.taskDetails.valid && this.isEditTask==false){
      var task:Task = {
        taskName : this.taskDetails.value.title,
        description : this.taskDetails.value.description
      }
      this.isLoading=true;
      this.taskService.addTask(task).subscribe(()=>{
        this.taskService.triggerReload();
        this.isLoading=false;
      }
      );
        this.isSaveClicked = true;
        this.isClicked.emit(this.isSaveClicked);
        this.toastr.success(Constants.taskAdded);
    }
    else if(this.taskDetails.valid && this.isEditTask){
      if(this.editTask){
        this.isEditTask=false;
        var task:Task = {
          taskId : this.editTask.taskId,
          taskName : this.taskDetails.value.title,
          description : this.taskDetails.value.description
        }
        this.isLoading=true;
        this.taskService.editTask(task).subscribe(
          task=>{
            this.taskService.triggerReload();
            this.toastr.success(Constants.taskUpdated);
            this.isLoading=false;
          }
        );
      }
      this.isSaveClicked = true;
        this.isClicked.emit(this.isSaveClicked);
        this.sharedService.clearData();
    }
  }

  cancelAddTask(){
    this.isCancelClicked = true;
    this.sharedService.clearData();
    this.isClicked.emit(this.isCancelClicked);
  }
}
