import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service'
import { TaskService } from '../_services/task.service'
import { UserService } from '../_services/user.service'
import { Task } from '../_models/task';
import { User } from '../_models/user';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  tasks: Task[];
  users: User[];

  newTask: string = '';
  newUser: string = '';

  loading = false;

  GetTasks(){
    this.taskService.GetTasks().subscribe(result => this.tasks = result as Task[]);
  }
  GetUsers(){
    this.userService.GetUsers().subscribe(result => this.users = result as User[]);
  }

  constructor(private authService : AuthenticationService, private userService : UserService, private taskService : TaskService) {
    this.GetTasks();
    this.GetUsers();  
  }

  ngOnInit() {
  }

  logOut(){
    this.authService.logout();
  }

  AddTask(){
    let task: Task = {
      TaskId: undefined,
      Name: this.newTask,
      Description: '',
      Day: '',
      User: null
    }
    this.loading = true;
    this.taskService.AddTask(task).subscribe(result =>{
      this.loading = false;
      this.newTask = '';
      this.GetTasks();
    });
  }
  AddUser(){
    let user: User = {
      UserId: undefined,
      FullName: this.newUser,
      Tasks: null
    }
    this.loading = true;
    this.userService.AddUser(user).subscribe(result =>{
      this.loading = false;
      this.newUser = '';
      this.GetUsers();
    });
  }

}
