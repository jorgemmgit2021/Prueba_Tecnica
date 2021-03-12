import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar, MatSnackBarConfig, MatCheckbox } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Ciclos } from '../models/ciclos';
import { CiclosService } from '../servicios/ciclos.service';

@Component({
  selector: 'app-ciclos-editar',
  templateUrl: './ciclos-editar.component.html',
  styleUrls: ['./ciclos-editar.component.css']
})
export class CiclosEditarComponent implements OnInit {

  public d: Ciclos;
  public p$: Observable<Ciclos[]>;
  public notyfy:boolean;
  public toNotify:string;

  constructor(private service:CiclosService, private route:ActivatedRoute, private router:Router,private  dialog: MatDialog, public snackBar:MatSnackBar){
    this.d = this.initType();
    this.notyfy = false;
    this.toNotify='';
  }
  initType(){
      this.d = { Id_Ciclo :0,Descripcion:'',Estado:false };
      return this.d;
  }
  ngOnInit(){
    var _id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    if(_id!=0){
      this.service.Editar(_id).subscribe((data:Ciclos)=>{ this.d = data; console.log(this.d);});
    }
    else {
      this.d = this.initType();
    }
  }
  public getCiclo(id){
    this.service.Editar(id).subscribe((data:Ciclos)=>{ if(this.d!=undefined) this.d = data; });
    console.log(this.d);
  }
  Guardar():void{
    this.toNotify = '';
    this.notyfy = null;
    let config = new MatSnackBarConfig();
    this.service.Create(this.d).subscribe((data:Ciclos)=>{ this.d = data;
      this.toNotify= `Registro de Ciclo completo`;
      this.snackBar.open(this.toNotify,'Cerrar', config);
    },error=>{
        this.toNotify= `Errores en los contenidos del formulario: ${error.message}`;
        this.snackBar.open(this.toNotify,'Cerrar', config);
      });
      this.notyfy=true;
  }
}
