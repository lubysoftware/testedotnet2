import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { NotificationService } from '../notification.service';
import { AuthClient, LoginModelRequest } from '../web-api-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public loggedIn = new BehaviorSubject<boolean>(false);

  constructor(private authClient: AuthClient, private router: Router, private notification: NotificationService) { }

  login(email: string, pass: string) {
    this.authClient.login(<LoginModelRequest>{
      userName: email,
      password: pass,
    }).subscribe(
      result => {
        if (result) {
          localStorage.setItem('token', result);

          this.loggedIn.next(true);
          this.router.navigate(['/home']);
          this.notification.showInfo('Bem vindo(a) ao Desafio Back-end Luby Software. ', '');
        }
        else {
          this.notification.showWarning('Por favor, verifique se email e senha informado estão corretos.', 'Login inválido');
        }
      },
      error => {
        if (error.status == 400)
          this.notification.showWarning('Por favor, verifique se email e senha informado estão corretos.', 'Login inválido');
        else
          console.log(error);
      }
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }
}
