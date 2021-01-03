import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { environment } from './../../../environments/environment';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Status } from '../models/status.enum';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (req.url.search(environment.api_url) === -1)
      return next.handle(req);

    const token = this.authService.getAuthorization();
    const headers = token ? req.headers.set('authorization', token) : req.headers;

    const authReq = req.clone({ headers });
    return next.handle(authReq)
      .pipe(
        catchError(err => {
          if (err instanceof HttpErrorResponse && err.status === Status.Unauthorized) {
            this.snackBar.open('Sessão expirou!', 'OK', { duration: 3000 });
            this.router.navigate(['login']);
          }
          return throwError(err);
        })
      );
  }
}
