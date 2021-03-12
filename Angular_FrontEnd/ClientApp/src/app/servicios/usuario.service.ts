import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  public baseURL = environment.usuariosURL;
  _usuario: Usuario;
  _usuarios:Usuario[];
  constructor(private httpClient: HttpClient, private router:Router){  }
  public getAll():Observable<Usuario[]>{  
    return this.httpClient.get<Usuario[]>(`${this.baseURL}`);
  }
  public async getBy(id:number):Promise<Usuario>{
    return this.httpClient.get<Usuario>(`${this.baseURL}/GetById/${id}`).toPromise();
    //.toPromise().then(result=>{_result = result;debugger});    
  }
  public Editar(id:number){
    return this.httpClient.get<Usuario>(`${this.baseURL}/${id}`);
  }
  public create(data): Observable<any>{
    return this.httpClient.post(`${this.baseURL}`, data);
  }
  public async Delete(id:number){
    return await this.httpClient.delete<Usuario>(`${this.baseURL}/${id}`);
  }
  public async ConsultarIdentificacion(identificacion:number):Promise<Usuario>{
    return await this.httpClient.get<Usuario>(`${this.baseURL}/GetByIdentificacion/${identificacion}`).toPromise();    
  }
  public ConsultarAsignatura(id:number):Observable<Usuario[]>{
    return this.httpClient.get<Usuario[]>(`${this.baseURL}/GetByAsignatura/${id}`);
  }
}