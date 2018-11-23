import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { AuthenticationService } from '../_services/authentication.service'
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  loginForm: FormGroup;
  constructor(private formBuilder: FormBuilder,private router: Router,private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: '',
      password: ''
  });
  }
  
  onSubmit() {
    
    this.authenticationService.login(this.loginForm.value.username, this.loginForm.value.password)
        .subscribe(
            data => {
                this.router.navigate(['home']);
            },
            error => {
              alert(error.error.error_description);
            });
}

}
