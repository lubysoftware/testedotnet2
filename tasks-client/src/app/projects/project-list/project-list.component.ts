import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ListSate } from 'src/app/shared/models/list-state';
import { Pagination } from 'src/app/shared/models/pagination';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ProjectService } from 'src/app/shared/services/project.service';
import { ProjectListDto } from '../dtos/project-list.dto';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {

  dataSource = new MatTableDataSource<ProjectListDto>();
  listState: ListSate;
  columns: string[];
  search: Pagination;
  
  constructor(
    private readonly projectService: ProjectService,
    private readonly dialogService: DialogService,
    private readonly snackBar: MatSnackBar
  ) { 
    this.search = { page: 1, limit: 10 } as Pagination;
    this.columns = ['title', 'actions'];
    this.listState = new ListSate();
  }

  ngOnInit(): void { 
    this.loadProjects();
  }

  loadProjects() {
    this.listState.reset();
    this.projectService.list(this.search).subscribe(result => {
      if (!result.success) {
        this.snackBar.open('Falha ao carregar lista de projetos', 'OK', { duration: 3000 });
        return;
      }
      this.dataSource = new MatTableDataSource<ProjectListDto>(result.data);
      this.listState.update(result);
    }, () => {
      this.snackBar.open('Falha ao carregar lista de projetos', 'OK', { duration: 3000 });
    });
  }
  
  pageChange(ev: PageEvent) {
    this.search.page = ev.pageIndex + 1;
    this.search.limit = ev.pageSize;
    this.loadProjects();
  }

  remove(id: string) {
    this.dialogService.confirmRemove('Tem certeza que deseja remover este projeto?').subscribe(confirmRemove => {
      if (!confirmRemove) return;

      this.projectService.delete(id).subscribe(result => {
        if (result.success) {
          this.snackBar.open('Projeto removido com Sucesso!', 'OK', { duration: 3000 });
          const projects = this.dataSource.data.filter(c => c.id !== id);
          this.listState.noItems = projects.length === 0;
          this.dataSource = new MatTableDataSource<ProjectListDto>(projects);
          return;
        }

        this.snackBar.open('Erro ao remover projeto!', 'OK', { duration: 3000 });
      }, () => {
        this.snackBar.open('Erro ao remover projeto!', 'OK', { duration: 3000 });
      });
    });
  }

}
