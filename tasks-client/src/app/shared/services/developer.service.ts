import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {

  private readonly url = `${environment.api_url}/developers`;
  
  constructor(
    private readonly http: HttpClient
  ) { }

  
}
