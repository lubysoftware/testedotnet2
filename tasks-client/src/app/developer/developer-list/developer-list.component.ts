import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ListSate } from 'src/app/shared/models/list-state';
import { Pagination } from 'src/app/shared/models/pagination';
import { DeveloperService } from 'src/app/shared/services/developer.service';
import { DeveloperListDto } from '../dtos/developer-list.dto';

@Component({
  selector: 'app-developer-list',
  templateUrl: './developer-list.component.html',
  styleUrls: ['./developer-list.component.scss']
})
export class DeveloperListComponent implements OnInit {

  dataSource = new MatTableDataSource<DeveloperListDto>();
  listState: ListSate;
  columns: string[];
  search: Pagination;

  constructor(
    private readonly developerService: DeveloperService,
    private readonly snackBar: MatSnackBar,
  ) { 
    this.search = { page: 1, limit: 10 } as Pagination;
    this.columns = ['name', 'actions'];
    this.listState = new ListSate();
  }

  ngOnInit(): void { 
    this.loadDevelopers();
  }

  loadDevelopers() {
    this.listState.reset();
    this.developerService.list(this.search).subscribe(result => {
      if (!result.success) {
        this.snackBar.open('Falha ao carregar lista de desenvolvedores', 'OK', { duration: 3000 });
        return;
      }
      this.dataSource = new MatTableDataSource<DeveloperListDto>(result.data);
      this.listState.update(result);
    }, () => {
      this.snackBar.open('Falha ao carregar lista de desenvolvedores', 'OK', { duration: 3000 });
    });
  }
  
  pageChange(ev: PageEvent) {
    this.search.page = ev.pageIndex + 1;
    this.search.limit = ev.pageSize;
    this.loadDevelopers();
  }

  remove(id: string) {

  }
}
