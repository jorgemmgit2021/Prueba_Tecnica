import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Calificaciones } from '../models/calificaciones';

@Injectable({
  providedIn: 'root'
})
export class CalificacionesService {
  URLCalificaciones = environment.calificacionesURL;
  
   constructor(private http:HttpClient){    
   }
   getAll():Observable<Calificaciones[]>{
     return this.http.get<Calificaciones[]>(this.URLCalificaciones);
   }
   getItem(id:number):Observable<Calificaciones[]>{
     return this.http.get<Calificaciones[]>(`${this.URLCalificaciones}/${id}`);
   }
   public Editar(id:number){
     return this.http.get<Calificaciones>(`${this.URLCalificaciones}/${id}`);
  }
  public Create(data):Observable<Calificaciones>{    
   return this.http.post<Calificaciones>(`${this.URLCalificaciones}`,data);
  }
  public eliminarCiclo(Calificacion:Calificaciones):Observable<Calificaciones>{
     return this.http.delete<Calificaciones>(`${this.URLCalificaciones}/${Calificacion.Id_Calificacion}`);
   }
}