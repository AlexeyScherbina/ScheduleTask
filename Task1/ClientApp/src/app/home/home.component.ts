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

  taskArray: Array<Array<Task>>;
  users: User[];

  newTask: string = '';
  newUser: string = '';

  loading = false;

  GetTasks(){
    this.taskArray = new Array<Array<Task>>();
    for(var i = 0; i < 6; i++){
      this.taskArray.push(new Array<Task>());
    }
    this.taskService.GetTasks().subscribe(result => {
      result.forEach(element => {
        if(element.User === null){
          element.User = new User();
        }
        switch(element.Day){
          case "Monday": this.taskArray[0].push(element); break;
          case "Tuesday": this.taskArray[1].push(element); break;
          case "Wednesday": this.taskArray[2].push(element); break;
          case "Thursday": this.taskArray[3].push(element); break;
          case "Friday": this.taskArray[4].push(element); break;
          default: this.taskArray[5].push(element); break;
        }
      });
      console.log(this.taskArray);
    });
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

  DeleteTask(task:Task){
    this.taskService.DeleteTask(task).subscribe(result => {
      this.taskArray[5].splice(this.taskArray[5].indexOf(task),1);
    });
  }
  DeleteUser(user:User){
    this.userService.DeleteUser(user).subscribe(result => {
      this.users.splice(this.users.indexOf(user),1);
    });
  }

  AddTask(){
    let task: Task = {
      TaskId: undefined,
      Name: this.newTask,
      Description: '',
      Day: '',
      User: new User()
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
