import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/operators';
import { LoginDto } from 'src/app/auth/dtos/login.dto';
import { TokenDto } from 'src/app/auth/dtos/token.dto';
import { DeveloperDetailDto } from 'src/app/developer/dtos/developer-detail.dto';
import { environment } from 'src/environments/environment';
import { Result } from '../models/result';
import { ResultType } from '../models/result-type';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentDeveloper?: DeveloperDetailDto | null;
  private readonly url = `${environment.api_url}/auth`;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) { }

  public login(login: LoginDto): Observable<ResultType<TokenDto>> {
    return this.http.post<ResultType<TokenDto>>(`${this.url}/login`, login)
      .pipe(tap(result => {
        if (!result.success) return;
        const { data } = result;
        this.currentDeveloper = {
          id: data.id,
          login: data.login,
          name: data.name
        } as DeveloperDetailDto;
        localStorage.setItem('token', data.token);
        localStorage.setItem('developer', btoa(JSON.stringify(this.currentDeveloper)));
      }));
  }

  public logout(): void {
    this.http.get<Result>(`${this.url}/logout`).subscribe(
      (result) => {
        if (!result.success) return;
        this.currentDeveloper = null;
        localStorage.removeItem('token');
        localStorage.removeItem('developer');
        this.router.navigate(['/login']);
      }
    );
  }

  public getCurrentDeveloper() : DeveloperDetailDto | null {
    if (this.currentDeveloper) return this.currentDeveloper;

    const developer = localStorage.getItem('developer');
    if (developer)
      this.currentDeveloper = JSON.parse(atob(developer)) as DeveloperDetailDto;

    if (!this.currentDeveloper) {
      this.router.navigate(['/login']);
      return null;
    }

    return this.currentDeveloper;
  }

  check(): boolean {
    return localStorage.getItem('token') ? true : false;
  }

  getAuthorization(): string | null {
    const token = localStorage.getItem('token');
    if (token) return `Bearer ${token}`;
    return null;
  }
}
