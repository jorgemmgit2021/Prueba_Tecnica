import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../servicios/usuario.service';
import { Usuario } from '../models/usuario';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  public c: Usuario;
  public f$: Observable<Usuario[]>;
  constructor(public service:UsuarioService, private router:Router) { }
  ngOnInit(){
    this.f$ = this.service.getAll();console.log(this.f$);
  }
  public Editar(id){
    this.service.Editar(id).subscribe((data:Usuario)=>{ this.c = data; });
  }
  public async Eliminar(id){    
    (await this.service.Delete(id)).subscribe((data: Usuario) => { this.c = data; console.log(data); });
    this.router.navigate(['/Pacientes']);
  }
}
