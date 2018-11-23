import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { AuthenticationService } from '../_services/authentication.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  constructor(private formBuilder: FormBuilder,private router: Router,private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username: '',
      password: '',
      repeat: ''
  });
  }
  
  onSubmit() {
    let username = this.registerForm.value.username;
    let password = this.registerForm.value.password
    this.authenticationService.register(username, password)
        .subscribe(
            data => {
              this.authenticationService.login(username,password).subscribe(
                data => {               
                    this.router.navigate(['home']);
                },
                error => {
                  alert(error.error.ModelState['']);
                });
            },
            error => {
              alert(error.error.ModelState['']);
            });
}

}
