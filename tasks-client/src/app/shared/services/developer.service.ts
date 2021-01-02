import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DeveloperCreateDto } from 'src/app/developer/dtos/developer-create.dto';
import { DeveloperDetailDto } from 'src/app/developer/dtos/developer-detail.dto';
import { DeveloperListDto } from 'src/app/developer/dtos/developer-list.dto';
import { DeveloperUpdateDto } from 'src/app/developer/dtos/developer-update.dto';
import { DeveloperRankingListDto } from 'src/app/developer/dtos/ranking/developer-ranking-list.dto';
import { DeveloperRankingSearchDto } from 'src/app/developer/dtos/ranking/developer-ranking-search.dto';
import { DeveloperWorkListDto } from 'src/app/developer/dtos/works/developer-work-list.dto';
import { DeveloperWorkSearchDto } from 'src/app/developer/dtos/works/developer-work-search.dto';
import { environment } from 'src/environments/environment';
import { Rest } from '../http/rest';
import { Pagination } from '../models/pagination';
import { Result } from '../models/result';
import { ResultType } from '../models/result-type';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {

  private readonly url = `${environment.api_url}/developers`;
  
  constructor(
    private readonly http: HttpClient
  ) { }

  public get(id: string): Observable<ResultType<DeveloperDetailDto>> {
    return this.http.get<ResultType<DeveloperDetailDto>>(`${this.url}/${id}`);
  }

  public list(pagination: Pagination): Observable<ResultType<DeveloperListDto[]>> {
    return this.http.get<ResultType<DeveloperListDto[]>>(`${this.url}`, Rest.GetParams(pagination));
  }

  public listWorks(search: DeveloperWorkSearchDto): Observable<ResultType<DeveloperWorkListDto[]>> {
    return this.http.get<ResultType<DeveloperWorkListDto[]>>(`${this.url}/works`, Rest.GetParams(search));
  }

  public ranking(search: DeveloperRankingSearchDto): Observable<ResultType<DeveloperRankingListDto[]>> {
    return this.http.get<ResultType<DeveloperRankingListDto[]>>(`${this.url}/ranking`, Rest.GetParams(search)); 
  }

  public create(developer: DeveloperCreateDto): Observable<Result> {
    return this.http.post<Result>(`${this.url}`, developer);
  }

  public update(developer: DeveloperUpdateDto): Observable<Result> {
    return this.http.put<Result>(`${this.url}/${developer.id}`, developer);
  }

  public delete(id: string): Observable<Result> {
    return this.http.delete<Result>(`${this.url}/${id}`);
  }
}
