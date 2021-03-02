import {Component, Inject, Injectable, Input} from  '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialog} from  '@angular/material/dialog';
import { Observable } from 'rxjs';
//import { Doctores } from '../models/doctores';
//import { ControlIntegral } from '../models/control-integral';
import { DialogInfo } from '../models/dialog-info';
import { Asignatura } from '../models/asignatura';
import { InscripcionAsignatura } from '../models/inscripcion-asignatura';
import { Usuario } from '../models/usuario';

@Component({
templateUrl:'modal-dialog.component.html'
})
export  class  ModalDialogComponent{
    @Input() id:string;
    private a$:Observable<Asignatura[]>;
    private i:InscripcionAsignatura;
    private l:Asignatura[];
    private t:Usuario[];
    private _type:number;
    constructor(private  dialogRef: MatDialogRef<ModalDialogComponent>, @Inject(MAT_DIALOG_DATA) public  data: DialogInfo){
      this.l = [];      
      this.i=data._i;
      this.l=this.data._listD;
    }
    ngOnInit(){
      this.dialogRef.updateSize("60%","90%");            
    }
    public closeMe(){
      debugger        
        this.dialogRef.close(this.i);
    }
}