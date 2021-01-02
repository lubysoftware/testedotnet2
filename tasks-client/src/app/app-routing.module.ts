import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './auth/login-form/login-form.component';
import { DeveloperListComponent } from './developer/developer-list/developer-list.component';
import { LayoutComponent } from './shared/components/layout/layout/layout.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginFormComponent },
  { path: '', component: LayoutComponent, canActivate: [AuthGuard], children: [
    { path: 'developers', component: DeveloperListComponent, canActivate: [AuthGuard] },
  ] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
