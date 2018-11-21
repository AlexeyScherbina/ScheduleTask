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
    const headers = new HttpHeaders({'Content-Type':'application/json'});
    return this.http.post(`http://localhost:50029/api/Task/AddTask`, task, {headers:headers});
  }
}
