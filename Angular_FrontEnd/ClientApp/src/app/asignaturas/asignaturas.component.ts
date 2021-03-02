import { Component, OnInit } from '@angular/core';
import { AsignaturaService } from '../servicios/asignatura.service';
import { Asignatura } from '../models/asignatura';
import { Router } from '@angular/router';

@Component({
  selector: 'app-asignaturas',
  templateUrl: './asignaturas.component.html',
  styleUrls: ['./asignaturas.component.css']
})
export class AsignaturasComponent implements OnInit {

  public c: Asignatura;
  public f: Asignatura[];
  constructor(public service:AsignaturaService, private router:Router) { }
  ngOnInit(){
    this.service.getAll().subscribe((data:Asignatura[])=>{ this.f = data; console.log(this.f); });
  }
  public Editar(id){
    this.service.Editar(id).subscribe((data:Asignatura)=>{ this.c = data;  console.log(this.f);});
  }
  public async Eliminar(id){    
    (await this.service.Delete(id)).subscribe((data: Asignatura) => { this.c = data; console.log(data); });
    this.router.navigate(['/asignaturas']);
  }

}
