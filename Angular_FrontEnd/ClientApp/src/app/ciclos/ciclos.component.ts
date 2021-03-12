import { Component, OnInit } from '@angular/core';
import { CiclosService } from '../servicios/ciclos.service';
import { Ciclos } from '../models/ciclos';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ciclos',
  templateUrl: './ciclos.component.html',
  styleUrls: ['./ciclos.component.css']
})
export class CiclosComponent implements OnInit {

  public c: Ciclos;
  public f: Ciclos[];
  constructor(public service:CiclosService, private router:Router) { }
  ngOnInit(){
    this.service.getAll().subscribe((data:Ciclos[])=>{ this.f = data; console.log(this.f); });
  }
  public Editar(id){
    this.service.Editar(id).subscribe((data:Ciclos)=>{ this.c = data;  console.log(this.f);});
  }
  public async Eliminar(id){    
    (await this.service.eliminarCiclo(id)).subscribe((data: Ciclos) => { this.c = data; console.log(data); });
    this.router.navigate(['/Cicloss']);
  }
}


