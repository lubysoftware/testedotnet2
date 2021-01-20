import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthClient, LoginModelRequest } from '../web-api-client';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel = { UserName: '', Password: '' }

  constructor(private authClient: AuthClient, private router: Router) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/');
  }

  onSubmit(form: NgForm) {
    this.authClient.login(<LoginModelRequest>{
      userName: form.value.UserName,
      password: form.value.Password,
    }).subscribe(
      result => {
        localStorage.setItem('token', result);

        if (result)
          this.router.navigateByUrl('/');
        else
          alert('Login inválido! Por favor, verifique se email e senha informado estão corretos.');
      },
      error => {
        if (error.status == 400)
          alert('Login inválido! Por favor, verifique se email e senha informado estão corretos.');
        else
          console.log(error);
      }
    );
  }
}
