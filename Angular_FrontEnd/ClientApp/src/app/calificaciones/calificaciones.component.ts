import { Component, OnInit } from '@angular/core';
import { CalificacionesService } from '../servicios/Calificaciones.service';
import { Calificaciones } from '../models/Calificaciones';
import { Router } from '@angular/router';
import { MatTableDataSource, MatTable } from '@angular/material';
import { Observable } from 'rxjs';
import { async } from '@angular/core/testing';

@Component({
  selector: 'app-calificaciones',
  templateUrl: './calificaciones.component.html',
  styleUrls: ['./calificaciones.component.css']
})
export class CalificacionesComponent implements OnInit {

  public c:Calificaciones;
  public f:Calificaciones[];
  public _service:CalificacionesService;
  constructor(public service:CalificacionesService, private router:Router) { 
  }
  ngOnInit(){
    this.service.getAll().subscribe((data:Calificaciones[])=>{ this.f = data; console.log(this.f); });
  }
  public Editar(id){
    this.service.Editar(id).subscribe((data:Calificaciones)=>{ this.c = data;});
  }
  public async Eliminar(id){
    (await this.service.eliminarCiclo(id)).subscribe((data: Calificaciones) => { this.c = data; console.log(data); });
    this.router.navigate(['/calificaciones']);
  }
}
