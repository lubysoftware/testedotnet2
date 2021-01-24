import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel = { UserName: '', Password: '' };

  constructor(private authService: AuthService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.authService.logout(); 
    }
  }

  onSubmit(form: NgForm) {
    this.authService.login(form.value.UserName, form.value.Password); 
  }
}
