import { Observable } from "rxjs";
import { InscripcionAsignatura } from "./inscripcion-asignatura";
import { Asignatura } from "./asignatura";
import { Usuario } from "./usuario";

export interface DialogInfo {
    _listD:Asignatura[],_listP:Usuario[], _i:InscripcionAsignatura, _type:number;
}
