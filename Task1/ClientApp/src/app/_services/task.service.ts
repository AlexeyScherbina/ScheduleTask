import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Task } from '../_models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient) { }

  GetTasks(){
    return this.http.get<Task[]>(`http://localhost:50029/api/Task/GetTasks`);
  }
  AddTask(task: Task){
    return this.http.post(`http://localhost:50029/api/Task/AddTask`, task);
  }
  DeleteTask(task: Task){
    return this.http.delete(`http://localhost:50029/api/Task/DeleteTask/` + task.TaskId );
  }
  AssignDay(task: Task){
    return this.http.post(`http://localhost:50029/api/Task/AssignDay`, task );
  }
  AssignUser(tid: number, uid: number){
    return this.http.post(`http://localhost:50029/api/Task/AssignUser`, {taskId: tid, userId: uid} );
  }
}
