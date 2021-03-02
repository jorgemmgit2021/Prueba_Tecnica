import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Asignatura } from '../models/asignatura';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AsignaturaService {

  public baseURL = environment.asignaturasURL;
  public d:Asignatura[];
  constructor(private httpClient:HttpClient, private route:ActivatedRoute, private router:Router){}
  public getAll():Observable<Asignatura[]>{    
    return this.httpClient.get<Asignatura[]>(`${this.baseURL}`);
  }
  public getBy(id:number):Observable<any[]>{    
    return this.httpClient.get<any[]>(`${this.baseURL}/GetById/${id}`);
  }
  public Editar(id:number){
      return this.httpClient.get<Asignatura>(`${this.baseURL}/${id}`);
  }
  public Create(data):Observable<Asignatura>{    
    return this.httpClient.post<Asignatura>(`${this.baseURL}`,data);    
  }
  public async Delete(id:number){
    return await this.httpClient.get(`${this.baseURL}/Delete/${id}`);
  }
}
