import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "./auth/auth.guard";
import { DesenvolvedorComponent } from "./desenvolvedor/desenvolvedor.component";
import { HomeComponent } from "./home/home.component";
import { LancamentoHorasComponent } from "./lancamento-horas/lancamento-horas.component";
import { LoginComponent } from "./login/login.component";
import { ProjetoComponent } from "./projeto/projeto.component";
import { RankingComponent } from "./ranking/ranking.component";
import { NgModule } from '@angular/core';
import { Role } from "./_models/user";
import { ForbiddenComponent } from "./forbidden/forbidden.component";

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'projeto', component: ProjetoComponent, canActivate: [AuthGuard], data: { permittedRoles: [Role.Admin] } },
  { path: 'desenvolvedor', component: DesenvolvedorComponent, canActivate: [AuthGuard], data: { permittedRoles: [Role.Admin] } },
  { path: 'ranking', component: RankingComponent, canActivate: [AuthGuard], data: { permittedRoles: [Role.Admin] } },
  { path: 'login', component: LoginComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: 'lancamento-horas', component: LancamentoHorasComponent, canActivate: [AuthGuard], data: { permittedRoles: [Role.Dev] } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
