import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Status } from 'src/app/shared/models/status.enum';
import { AuthService } from 'src/app/shared/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoginDto } from '../dtos/login.dto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  form: FormGroup;
  loading: boolean = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar
  ) { 
    this.form = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  onSubmit() { 
    this.loading = true;
    var loginDto = this.form.getRawValue() as LoginDto;
    this.authService.login(loginDto).subscribe(result => {
      if (!result.success) {
       this.snackBar.open('Erro ao realizar login', 'OK', { duration: 3000 });
       return;
      }

      this.loading = false;
      this.router.navigate(['/']);
    }, (error: HttpErrorResponse) => {
      if (error.status === Status.Invalid) {
        this.snackBar.open('Informe o login e a senha', 'OK', { duration: 3000 });
        return;
      }
      if (error.status === Status.Unauthorized) {
        this.snackBar.open('Usuário ou Senha Incorreto', 'OK', { duration: 3000 });
        return;
      }
      this.snackBar.open('Erro ao realizar login', 'OK', { duration: 3000 });
    });
  }

  isFieldInvalid(field: string) {
    return (
      (!this.form.get(field)?.valid && this.form.get(field)?.touched) ||
      (this.form.get(field)?.untouched )
    );
  }
}
