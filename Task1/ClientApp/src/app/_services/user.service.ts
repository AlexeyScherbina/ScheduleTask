import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  GetUsers(){
    return this.http.get<User[]>(`http://localhost:50029/api/User/GetUsers`);
  }
  AddUser(user: User){
    const headers = new HttpHeaders({'Content-Type':'application/json'});
    return this.http.post(`http://localhost:50029/api/User/AddUser`, user, {headers:headers});
  }
  DeleteUser(user: User){
    return this.http.delete(`http://localhost:50029/api/User/DeleteUser/` + user.UserId);
  }
}
