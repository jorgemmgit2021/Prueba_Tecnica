
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Ciclos } from '../models/ciclos';

@Injectable({
  providedIn: 'root'
})
export class CiclosService {
 URLCiclos = environment.ciclosURL;

  constructor(private http:HttpClient){    
  }
  getAll():Observable<Ciclos[]>{
    return this.http.get<Ciclos[]>(this.URLCiclos);
  }
  getItem(id:number):Observable<Ciclos[]>{
    return this.http.get<Ciclos[]>(`${this.URLCiclos}/${id}`);
  }
  public Editar(id:number){
    return this.http.get<Ciclos>(`${this.URLCiclos}/${id}`);
}
public Create(data):Observable<Ciclos>{    
  return this.http.post<Ciclos>(`${this.URLCiclos}`,data);    
}
public eliminarCiclo(Ciclo:Ciclos):Observable<Ciclos>{
    return this.http.delete<Ciclos>(`${this.URLCiclos}/${Ciclo.Id_Ciclo}`);
  }
}
