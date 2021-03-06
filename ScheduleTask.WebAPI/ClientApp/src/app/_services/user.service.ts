import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../_models/user';
import { Config } from '../url.config'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  GetUsers(){
    return this.http.get<User[]>(Config.baseUrl +  `/api/User/GetUsers`);
  }
  AddUser(user: User){
    const headers = new HttpHeaders({'Content-Type':'application/json'});
    return this.http.post(Config.baseUrl +  `/api/User/AddUser`, user, {headers:headers});
  }
  DeleteUser(user: User){
    return this.http.delete(Config.baseUrl +  `/api/User/DeleteUser/` + user.UserId);
  }
}
