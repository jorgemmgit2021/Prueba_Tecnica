import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar, MatSnackBarConfig } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Asignatura } from '../models/asignatura';
import { Usuario } from '../models/usuario';
import { AsignaturaService } from '../servicios/asignatura.service';

@Component({
  selector: 'app-asignaturas-editar',
  templateUrl: './asignaturas-editar.component.html',
  styleUrls: ['./asignaturas-editar.component.css']
})
export class AsignaturasEditarComponent implements OnInit {
  public d: Asignatura;
  public p$: Observable<Asignatura[]>;
  public l$: Observable<Usuario[]>;
  public notyfy:boolean;
  public toNotify:string;
  listService: any;

  constructor(private service:AsignaturaService, private route:ActivatedRoute, private router:Router,private  dialog: MatDialog, public snackBar:MatSnackBar){
    this.d = this.initType();
    this.notyfy = false;
    this.toNotify='';
  }
  initType(){
      this.d = { Id_Asignatura :0,Descripcion:'',Creditos:0,Id_Usuario:0 };
      return this.d;
  }
  start():void{ 
  }
  end(x):void{
  }
  ngOnInit(){
    var _id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    if(_id!=0){
      this.service.Editar(_id).subscribe((data:Asignatura)=>{ this.d = data; console.log(this.d);});
    }
    else {
      this.d = this.initType();
    }
    this.p$= this.listService.getBy(_id);
    console.log(this.p$);
    console.log(this.l$);
  }
  public getAsignatura(id){
    this.service.Editar(id).subscribe((data:Asignatura)=>{ if(this.d!=undefined) this.d = data; });
    console.log(this.d);
  }
  Guardar():void{
    this.toNotify = '';
    this.notyfy = null;
    let config = new MatSnackBarConfig();
    this.service.Create(this.d).subscribe((data:Asignatura)=>{ this.d = data;
      this.toNotify= `Registro de asignatura completo`;
      this.snackBar.open(this.toNotify,'Cerrar', config);
    },error=>{
        this.toNotify= `Errores en los contenidos del formulario: ${error.message}`;
        this.snackBar.open(this.toNotify,'Cerrar', config);
      });
      this.notyfy=true;
  }
}