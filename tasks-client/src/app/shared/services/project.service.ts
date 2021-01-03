import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProjectCreateDto } from 'src/app/projects/dtos/project-create.dto';
import { ProjectDetailDto } from 'src/app/projects/dtos/project-detail.dto';
import { ProjectListDto } from 'src/app/projects/dtos/project-list.dto';
import { ProjectUpdateDto } from 'src/app/projects/dtos/project-update.dto';
import { ProjectWorkDetailDto } from 'src/app/projects/dtos/works/project-work-detail.dto';
import { ProjectWorkListDto } from 'src/app/projects/dtos/works/project-work-list.dto';
import { ProjectWorkSearchDto } from 'src/app/projects/dtos/works/project-work-search.dto';
import { WorkDto } from 'src/app/works/dtos/work.dto';
import { environment } from 'src/environments/environment';
import { Rest } from '../http/rest';
import { Pagination } from '../models/pagination';
import { Result } from '../models/result';
import { ResultType } from '../models/result-type';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private readonly url = `${environment.api_url}/projects`;

  constructor(
    private readonly http: HttpClient
  ) { }

  get(id: string): Observable<ResultType<ProjectDetailDto>> {
    return this.http.get<ResultType<ProjectDetailDto>>(`${this.url}/${id}`);
  }

  list(pagination: Pagination): Observable<ResultType<ProjectListDto[]>> {
    return this.http.get<ResultType<ProjectListDto[]>>(this.url, Rest.GetParams(pagination));
  }

  create(project: ProjectCreateDto): Observable<Result> {
    return this.http.post<Result>(this.url, project);
  }

  update(project: ProjectUpdateDto): Observable<Result> {
    return this.http.put<Result>(`${this.url}/${project.id}`, project);
  }

  delete(id: string): Observable<Result> {
    return this.http.delete<Result>(`${this.url}/${id}`);
  }

  getWork(id: string, projectId: string): Observable<ResultType<ProjectWorkDetailDto>> {
    return this.http.get<ResultType<ProjectWorkDetailDto>>(`${this.url}/${projectId}/works/${id}`);
  }

  listWorks(search: ProjectWorkSearchDto): Observable<ResultType<ProjectWorkListDto[]>> {
    return this.http.get<ResultType<ProjectWorkListDto[]>>(`${this.url}/works`, Rest.GetParams(search));
  }

  createWork(work: WorkDto): Observable<Result> {
    return this.http.post<Result>(`${this.url}/${work.projectId}/works`, work);
  }

  updateWork(work: WorkDto): Observable<Result> {
    return this.http.put<Result>(`${this.url}/${work.projectId}/works/${work.id}`, work);
  }

  deleteWork(projectId: string, id: string): Observable<Result>{
    return this.http.delete<Result>(`${this.url}/${projectId}/works/${id}`);
  }
}
