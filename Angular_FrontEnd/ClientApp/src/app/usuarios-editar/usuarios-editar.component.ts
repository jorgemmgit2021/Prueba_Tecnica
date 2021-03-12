import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { formatNumber } from '@angular/common';
import { ModalDialogComponent } from '../modal-dialog/modal-dialog.component';
import { MatDialog, MatDialogConfig, MatDialogRef, MatSnackBarConfig, MatSnackBarModule, MatSelect } from  '@angular/material';
import { Observable } from 'rxjs';
import { DialogInfo } from '../models/dialog-info';
import { config } from 'process';
import { MatSnackBar} from '@angular/material';
import { Usuario } from '../models/usuario';
import { UsuarioService } from '../servicios/usuario.service';
import { AsignaturaService } from '../servicios/asignatura.service';
import { InscripcionAsignatura } from '../models/inscripcion-asignatura';
import { Asignatura } from '../models/asignatura';

@Component({
  selector: 'app-usuarios-editar',
  templateUrl: './usuarios-editar.component.html',
  styleUrls: ['./usuarios-editar.component.css']
})
export class UsuariosEditarComponent implements OnInit {
  public c: Usuario;
  public a$: Observable<Asignatura[]>;
  public l$: Observable<InscripcionAsignatura[]>;
  public i: InscripcionAsignatura;
  public k: any[];
  public tipo:any;
  public notyfy:boolean;
  public toNotify:string;

  initType(){
    this.c = { 
        Id_Usuario:0,
        Numero_Identificacion:0,
        Nombre_Completo:'',
        Tipo_Usuario:0,
        Fecha:new Date().toISOString(),
        Asignaturas:[]
      };
      return this.c;
    }
    initSub(): InscripcionAsignatura {
      this.i = { Id_Asignatura:0, Id_Usuario:0, Id_Materia:0, Id_Ciclo:0};
      return this.i;
    }
    constructor(private service:UsuarioService, private listService:AsignaturaService, private route:ActivatedRoute, 
      private router:Router,private  dialog: MatDialog, public snackBar:MatSnackBar){
      this.c = this.initType();
      this.i = this.initSub();
      this.notyfy = false;
      this.toNotify='';
      this.tipo=[{Id:1,Descripcion:'Alumno'},{Id:2,Descripcion:'Docente'}];
    }
  start():void{
    this.i.Id_Usuario = this.c.Id_Usuario;
    this.dialog.open(ModalDialogComponent, {data:{ _i:this.i, _listD: this.a$, _listP:this.l$, _type:1 }});   
    this.dialog.afterAllClosed.subscribe(x=>this.end(x));
  }
  end(x):void{
    if(this.c.Tipo_Usuario!=1){this.snackBar.open('El registro de Docentes no puede incluir asignaturas','Cerrar', new MatSnackBarConfig());return;}
    let _cont:InscripcionAsignatura[] =this.c.Asignaturas||[];
    let _x:InscripcionAsignatura = { Id_Asignatura: this.i.Id_Asignatura, Id_Materia:Number(this.i.Id_Materia), Id_Usuario:Number(this.i.Id_Usuario), Id_Ciclo :this.i.Id_Ciclo};    
    let u = this.c.Asignaturas.find(function(indx){ return indx.Id_Materia== _x.Id_Materia && indx.Id_Usuario==_x.Id_Usuario; });
    if(u==undefined){      
      _cont.unshift(_x)
      _x = null;
    }
    this.c.Asignaturas = _cont;
    _cont = null;
  }
  ngOnInit(){
    var _id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    if(_id!=0)this.service.Editar(_id).subscribe((data:Usuario)=>{ this.c = data; console.log(this.c);});
    else this.c = this.initType();
    this.a$ = this.listService.getAll(); 
    this.listService.getBy(_id).toPromise().then(val=>{
      this.k = [];
      val.forEach(v=>{ 
        this.k.push(v);
      });
    });
    console.log(this.a$);
    console.log(this.k);
  }
  public getPacientes(id){
    this.service.Editar(id).subscribe((data:Usuario)=>{ if(this.c!=undefined) this.c = data; });
    console.log(this.c);
  }
  Guardar():void{
    debugger
    this.toNotify = '';
    this.notyfy = null;
    let config = new MatSnackBarConfig();
    this.service.create(this.c).subscribe((data:Usuario)=>{ this.c = data;
      this.toNotify= `Registro de Usuario completo`;
      this.snackBar.open(this.toNotify,'Cerrar', config);
    },error=>{
        this.toNotify= `Errores en los contenidos del formulario:${error.error.split(':')[1]}`;
        this.snackBar.open(this.toNotify,'Cerrar', config);
      });
      this.notyfy=true;
  }
}
