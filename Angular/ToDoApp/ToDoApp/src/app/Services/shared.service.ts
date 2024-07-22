import { Injectable } from '@angular/core';
import {Observable, ReplaySubject } from 'rxjs';
import { Task } from '../Interfaces/task';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private dataSubject : ReplaySubject<Task | null> = new ReplaySubject<Task | null>(1);
  private taskIdSubject : ReplaySubject<number> = new ReplaySubject<number>(1);

  constructor() { }

  setData(taskData:Task){
   this.dataSubject.next(taskData); 
  }

  getData():Observable<Task | null>{
    return this.dataSubject.asObservable();
  }
  clearData(){
    this.dataSubject.next(null); 
   }

   setTaskIdData(taskId : number){
    this.taskIdSubject.next(taskId);
   }

   getTaskId(){
    return this.taskIdSubject.asObservable();
   }
}
