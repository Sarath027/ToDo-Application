import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from '../Interfaces/task';
import { UserTask } from '../Interfaces/user-task';
import { Subject } from 'rxjs';
import { Api } from '../constants/api';
import { TaskPercentages } from '../Interfaces/task-percentages';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  task!:Task;
  constructor(private http : HttpClient) { }

  private reloadSubject = new Subject<void>();
  get reloadObservable(){
    return this.reloadSubject.asObservable();
  }
  triggerReload(){
    this.reloadSubject.next();
  }

  addTask(task : Task){
    console.log("Add Task");
   return this.http.post(Api.taskEndpoint+'/AddTask', task);
  }

  getAllTasks(){
    return this.http.get<UserTask[]>(Api.taskEndpoint);
  }

  activeTasks(){
    return this.http.get<UserTask[]>(Api.taskEndpoint+'/ActiveTasks');
  }

  completedTasks(){
    return this.http.get<UserTask[]>(Api.taskEndpoint+'/CompletedTasks');
  }

  activeToCompleteTask(taskId : number){
    return this.http.get<UserTask[]>(Api.taskEndpoint+`/ToggleTaskStatus?taskId=${taskId}`);
  }

  deleteAllTask(){
    return this.http.delete<UserTask[]>(Api.taskEndpoint+'/DeleteAll');
  }

  deleteTask(taskId : number){
    return this.http.delete<UserTask[]>(Api.taskEndpoint+`?taskId=${taskId}`);
  }

  getTaskStatus(){
    return this.http.get<TaskPercentages>(Api.taskEndpoint+'/TaskStatus');
  }

  editTask(task:Task){
    return this.http.put<UserTask[]>(Api.taskEndpoint,task);
  }
}
