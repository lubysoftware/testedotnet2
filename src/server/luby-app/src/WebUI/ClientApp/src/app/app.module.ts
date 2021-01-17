import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProjetoComponent } from './projeto/projeto.component';
import { DesenvolvedorComponent } from './desenvolvedor/desenvolvedor.component';
import { LancamentoHorasComponent } from './lancamento-horas/lancamento-horas.component';
import { RankingComponent } from './ranking/ranking.component';
import { LoginComponent } from '../api-authorization/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    ProjetoComponent,
    DesenvolvedorComponent,
    LancamentoHorasComponent,
    RankingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'projeto', component: ProjetoComponent, canActivate: [AuthorizeGuard] },
      { path: 'desenvolvedor', component: DesenvolvedorComponent, canActivate: [AuthorizeGuard] },
      { path: 'ranking', component: RankingComponent, canActivate: [AuthorizeGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'lancamento-horas', component: LancamentoHorasComponent, canActivate: [AuthorizeGuard] },
      { path: 'todo', component: TodoComponent /*, canActivate: [AuthorizeGuard]*/ },
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
