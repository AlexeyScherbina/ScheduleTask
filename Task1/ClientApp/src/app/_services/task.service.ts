import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Task } from '../_models/task';
import { Config } from '../url.config'

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient) { }

  GetTasks(){
    return this.http.get<Task[]>(Config.baseUrl +  `/api/Task/GetTasks`);
  }
  AddTask(task: Task){
    return this.http.post(Config.baseUrl +  `/api/Task/AddTask`, task);
  }
  DeleteTask(task: Task){
    return this.http.delete(Config.baseUrl +  `/api/Task/DeleteTask/` + task.TaskId );
  }
  AssignDay(task: Task){
    return this.http.post(Config.baseUrl +  `/api/Task/AssignDay`, task );
  }
  AssignUser(tid: number, uid: number){
    return this.http.post(Config.baseUrl +  `/api/Task/AssignUser`, {taskId: tid, userId: uid} );
  }
}
