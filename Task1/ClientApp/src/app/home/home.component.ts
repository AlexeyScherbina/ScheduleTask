import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service'
import { TaskService } from '../_services/task.service'
import { UserService } from '../_services/user.service'
import { Task } from '../_models/task';
import { User } from '../_models/user';
import { CdkDragDrop, transferArrayItem } from '@angular/cdk/drag-drop';



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

  onChange(value, task:Task){
    this.taskService.AssignUser(task.TaskId, value).subscribe(result => {});
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer !== event.container) {
      transferArrayItem(event.previousContainer.data,event.container.data,
        event.previousIndex, event.currentIndex)

      var container = 5;
      switch(event.container.id){
        case "cdk-drop-list-0":event.container.data[event.currentIndex]['Day'] = ''; container = 5; break;
        case "cdk-drop-list-1":event.container.data[event.currentIndex]['Day'] = 'Monday'; container = 0; break;
        case "cdk-drop-list-2":event.container.data[event.currentIndex]['Day'] = 'Tuesday'; container = 1; break;
        case "cdk-drop-list-3":event.container.data[event.currentIndex]['Day'] = 'Wednesday'; container = 2; break;
        case "cdk-drop-list-4":event.container.data[event.currentIndex]['Day'] = 'Thursday'; container = 3; break;
        case "cdk-drop-list-5":event.container.data[event.currentIndex]['Day'] = 'Friday'; container = 4; break;
        default: break;
      }
      this.taskService.AssignDay(this.taskArray[container][event.currentIndex])
      .subscribe(result => {});
    }
  }

  GetTasks(){
    this.taskArray = new Array<Array<Task>>();
    for(var i = 0; i < 6; i++){
      this.taskArray.push(new Array<Task>());
    }
    this.taskService.GetTasks().subscribe(result => {
      result.forEach(element => {
        if(element.User === null){
          element.User = new User();
          element.User.UserId = 0;
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
      this.GetTasks();
      this.GetUsers();
      //this.taskArray[5].splice(this.taskArray[5].indexOf(task),1);
    });
  }
  DeleteUser(user:User){
    this.userService.DeleteUser(user).subscribe(result => {
      this.GetTasks();
      this.GetUsers();
      //this.users.splice(this.users.indexOf(user),1);
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
    console.log(this.taskArray);
    let user: User = {
      UserId: undefined,
      FullName: this.newUser,
      Tasks: null
    }
    this.loading = true;
    this.userService.AddUser(user).subscribe(result =>{
      this.loading = false;
      this.newUser = '';
      console.log(this.taskArray);
      this.GetUsers();
      console.log(this.taskArray);
    });
  }

}
