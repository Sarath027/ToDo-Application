import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { TaskService } from '../Services/task.service';
import { OnChanges } from '@angular/core';
import { TaskPercentages } from '../Interfaces/task-percentages';

@Component({
  selector: 'app-task-status',
  standalone: true,
  imports: [],
  templateUrl: './task-status.component.html',
  styleUrl: './task-status.component.css'
})
export class TaskStatusComponent implements OnInit, OnChanges {
  @Input() isDeleteClicked! : boolean;
  taskStatus : TaskPercentages = {activePercent:0, completedPercent:0};
  constructor(private taskService : TaskService){}

  ngOnInit(): void {
    this.taskService.reloadObservable.subscribe(()=>{
      this.loadComponent();
    })
    this.loadComponent();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.isDeleteClicked){
      this.taskStatus.activePercent=0;
      this.taskStatus.completedPercent=0;
    }
  }

  loadComponent(){
    this.taskService.getTaskStatus().subscribe(
      taskStatus=>{
        console.log(taskStatus);
        this.taskStatus.activePercent=taskStatus.activePercent,
        this.taskStatus.completedPercent= taskStatus.completedPercent
      }
    )
  }
}
