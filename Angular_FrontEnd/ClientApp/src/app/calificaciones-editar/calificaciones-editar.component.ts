import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar, MatSnackBarConfig, MatCheckbox } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Asignatura } from '../models/asignatura';
import { Calificaciones } from '../models/calificaciones';
import { CalificacionesService } from '../servicios/calificaciones.service';
import { AsignaturaService } from '../servicios/asignatura.service';
import { Usuario } from '../models/usuario';
import { UsuarioService } from '../servicios/usuario.service';

@Component({
  selector: 'app-calificaciones-editar',
  templateUrl: './calificaciones-editar.component.html',
  styleUrls: ['./calificaciones-editar.component.css']
})
export class CalificacionesEditarComponent implements OnInit {

  public d: Calificaciones;
  public p$: Observable<Calificaciones[]>;
  public a: Asignatura[];
  public o: Usuario[];
  public n: Usuario;
  public _id:number;
  public _idV:number;
  public notyfy:boolean;
  public toNotify:string;
  _lService:AsignaturaService;
  _qService:UsuarioService;

  constructor(private service:CalificacionesService, listService:AsignaturaService, queryService:UsuarioService, private route:ActivatedRoute, private router:Router,private  dialog: MatDialog, public snackBar:MatSnackBar){
    this.d = this.initType();
    this.notyfy = false;
    this.toNotify='';
    this._lService = listService;
    this._lService.getAll().subscribe((result:Asignatura[])=>{this.a=result});
    this._qService = queryService;
    console.log(this.a);
  }
  initType(){
      this.d = { Id_Calificacion :0,Id_Asignatura:0, Nota:0.0, Fecha_Calificacion:new Date(), Estado:false };
      return this.d;
  }
  ngOnInit(){
    var _id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    if(_id!=0){
      this.service.Editar(_id).subscribe((data:Calificaciones)=>{ this.d = data; console.log(this.d);});
    }
    else {
      this.d = this.initType();
    }
  }
  public getCalificacion(id){
    this.service.Editar(id).subscribe((data:Calificaciones)=>{ if(this.d!=undefined) this.d = data; });
    console.log(this.d);
  }

  public onChange=(e)=>{
    this._idV = e.value;
    this._qService.ConsultarAsignatura(this._idV).subscribe((result:Usuario[])=>{ this.o=result; });
  }
  public asignarUsuario=async (id)=>{
    this._id = id;
    await this._qService.getBy(id).then((result:Usuario)=>{this.n = result[0]});
    this.d.Id_Asignatura =
    this.n.Asignaturas.find(t=>t.Id_Materia==this._idV).Id_Asignatura;
    this.o = [];
  }
  Guardar():void{
    this.toNotify = '';
    this.notyfy = null;
    let config = new MatSnackBarConfig();
    this.service.Create(this.d).subscribe((data:Calificaciones)=>{ this.d = data;
      this.toNotify= `Registro de Calificaciónes completo`;
      this.snackBar.open(this.toNotify,'Cerrar', config);
    },error=>{
        this.toNotify= `Errores en los contenidos del formulario: ${error.message}`;
        this.snackBar.open(this.toNotify,'Cerrar', config);
      });
      this.notyfy=true;
  }
}
