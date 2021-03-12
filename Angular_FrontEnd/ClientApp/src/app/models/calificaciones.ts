import { DecimalPipe } from "@angular/common";

export interface Calificaciones {
    Id_Calificacion: number,
    Id_Asignatura: number,
    Nota: number|DecimalPipe,
    Fecha_Calificacion: Date,
    Estado: boolean
}
