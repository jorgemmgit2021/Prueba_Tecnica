import { Component, OnInit } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { MatDialog, MatDialogConfig, MatDialogRef, MatSnackBarConfig, MatSnackBarModule, MatSelect } from  '@angular/material';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario';
import { Asignatura } from '../models/asignatura';
import { Consultas } from '../models/consultas';
import { InscripcionAsignatura } from '../models/inscripcion-asignatura';
import { AsignaturaService } from '../servicios/asignatura.service';
import { UsuarioService } from '../servicios/usuario.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Asignaturadescripcion } from '../models/asignaturadescripcion';

@Component({
  selector: 'app-consultas',
  templateUrl: './consultas.component.html',
  styleUrls: ['./consultas.component.css']
})
export class ConsultasComponent{
  public _consulta:Consultas;
  public _Usuario: Usuario;
  public _Usuarios: Usuario[];
  public _Inscripciones: InscripcionAsignatura[];
  public _Asignatura: any;
  public _Asignaturas: Asignaturadescripcion[];
  public _Estudiantes: Usuario[];
  public tipo:any;
  initType(){
    this._Usuario = { 
        Id_Usuario:0,
        Numero_Identificacion:0,
        Nombre_Completo:'',
        Tipo_Usuario:0,
        Fecha:new Date().toISOString(),
        Asignaturas:[]
       };
      return this._Usuario;
  }
  constructor(private serviceUsuario:UsuarioService, private serviceAsignatura:AsignaturaService, private router:Router){      
    this.tipo=[{Id:1,Descripcion:'Alumno'},{Id:2,Descripcion:'Docente'}];
    this._Asignaturas = [];
    this.initType();
  }
  ngOnInit(){
    this.initType();
    this._Inscripciones=[];
    this._Asignaturas = [];
  }
  async consultarUsuario(e){
      this.serviceUsuario.ConsultarIdentificacion(this._Usuario.Numero_Identificacion).then(v=>{ this._Usuario=v });
      this._Inscripciones = this._Usuario.Asignaturas;
      this.serviceAsignatura.getBy(this._Usuario.Id_Usuario).subscribe(val=>{ this._Asignaturas = val; });
      console.warn(this);
  }
  async consultarEstudiantes(id:number){
    this.serviceUsuario.ConsultarAsignatura(id).subscribe(v=>{ this._Estudiantes = v });
  }
}
