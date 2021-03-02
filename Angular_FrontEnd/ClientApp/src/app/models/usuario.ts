import { Asignatura } from "./asignatura";
import { InscripcionAsignatura } from "./inscripcion-asignatura";

export interface Usuario {
    Id_Usuario:number,
    Numero_Identificacion:number,
    Nombre_Completo:string,
    Tipo_Usuario:number,
    Fecha:string
    Asignaturas:InscripcionAsignatura[]
}
