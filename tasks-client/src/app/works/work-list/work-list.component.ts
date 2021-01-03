import { Component, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ProjectWorkListDto } from 'src/app/projects/dtos/works/project-work-list.dto';
import { ProjectWorkSearchDto } from 'src/app/projects/dtos/works/project-work-search.dto';
import { AutocompleteComponent } from 'src/app/shared/components/autocomplete/autocomplete.component';
import { ListSate } from 'src/app/shared/models/list-state';
import { AuthService } from 'src/app/shared/services/auth.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ProjectService } from 'src/app/shared/services/project.service';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.scss']
})
export class WorkListComponent implements OnInit {

  dataSource = new MatTableDataSource<ProjectWorkListDto>();
  showSearch = false;
  listState: ListSate;
  columns: string[];
  search: ProjectWorkSearchDto;
  
  @ViewChild('projects', { static: false }) projects: AutocompleteComponent;

  constructor(
    private readonly authService: AuthService,
    private readonly projectService: ProjectService,
    private readonly dialogService: DialogService,
    private readonly snackBar: MatSnackBar
  ) { 
    this.search = { page: 1, limit: 10, viewAll: false, projectId: null } as ProjectWorkSearchDto;
    this.columns = ['comment', 'startTime', 'endTime', 'project', 'developer', 'hours', 'actions'];
    this.listState = new ListSate();
  }

  ngOnInit(): void {
    this.loadWorks();
    this.loadProjects();
  }

  loadWorks() {
    this.listState.reset();
    this.search.developerId = this.search.viewAll ? null : this.authService.getCurrentDeveloper()?.id ?? null;
    this.projectService.listWorks(this.search).subscribe(result => {
      if (!result.success) {
        this.snackBar.open('Falha ao carregar lista de atividades', 'OK', { duration: 3000 });
        return;
      }
      this.dataSource = new MatTableDataSource<ProjectWorkListDto>(result.data);
      this.listState.update(result);
    }, () => {
      this.snackBar.open('Falha ao carregar lista de atividades', 'OK', { duration: 3000 });
    });
  }

  loadProjects() {
    this.projectService.list({ page: 1, limit: 10 }).subscribe(result => {
      if (!result.success) {
        this.snackBar.open('Falha ao carregar lista de projetos', 'OK', { duration: 3000 });
        return;
      }
      this.projects.setList(result.data);
    }, () => {
      this.snackBar.open('Falha ao carregar lista de projetos', 'OK', { duration: 3000 });
    });
  }
  
  pageChange(ev: PageEvent) {
    this.search.page = ev.pageIndex + 1;
    this.search.limit = ev.pageSize;
    this.loadWorks();
  }

  remove(id: string, projectId: string) {
    this.dialogService.confirmRemove('Tem certeza que deseja remover a atividade?').subscribe(confirmRemove => {
      if (!confirmRemove) return;

      this.projectService.deleteWork(id, projectId).subscribe(result => {
        if (result.success) {
          this.snackBar.open('Atividade removida com Sucesso!', 'OK', { duration: 3000 });
          const works = this.dataSource.data.filter(c => c.id !== id);
          this.listState.noItems = works.length === 0;
          this.dataSource = new MatTableDataSource<ProjectWorkListDto>(works);
          return;
        }

        this.snackBar.open('Erro ao remover atividade!', 'OK', { duration: 3000 });
      }, () => {
        this.snackBar.open('Erro ao remover atividade!', 'OK', { duration: 3000 });
      });
    });
  }
  
  onShowSearch() {
    document.querySelector('mat-sidenav-content')?.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
    this.showSearch = true;
  }

  onHideSearch() {
    this.listState.reset();
    this.showSearch = false;
    this.dataSource = new MatTableDataSource<ProjectWorkListDto>([]);
    this.search.viewAll = false;
    this.search.projectId = null;
    this.search.page = 1;
    this.loadWorks();
  }

  formatDateTime(date: string): string {
    const dateObj = new Date(date);
    const dateStr = dateObj.toLocaleDateString('pt-Br');
    const timeStr = dateObj.toLocaleTimeString('pt-Br');
    return `${dateStr} ${timeStr}`;
  }
}
