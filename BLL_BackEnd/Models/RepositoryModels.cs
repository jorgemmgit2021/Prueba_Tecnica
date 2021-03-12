using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BLL_BackEnd.Models {
    public class Usuario {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Usuario")]
        [JsonPropertyName("Id_Usuario")]
        public int Id_Usuario { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Numero_Identificacion")]
        [JsonPropertyName("Numero_Identificacion")]
        public int Numero_Identificacion { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Nombre_Completo")]
        [JsonPropertyName("Nombre_Completo")]
        public string Nombre_Completo { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Tipo_Usuario")]
        [JsonPropertyName("Tipo_Usuario")]
        public int Tipo_Usuario { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Fecha")]
        [JsonPropertyName("Fecha")]
        public DateTime Fecha { get; set; }
        [JsonPropertyName("Asignaturas")]
        [ForeignKey("Id_Usuario")]
        public List<Inscripcion_Asignatura> Asignaturas { get; set; }
    }
    public class Asignatura {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Asignatura")]
        [JsonPropertyName("Id_Asignatura")]
        public int Id_Asignatura { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Descripcion")]
        [JsonPropertyName("Descripcion")]
        public string Descripcion { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Creditos")]
        [JsonPropertyName("Creditos")]
        public int Creditos { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Usuario")]
        [ForeignKey("Id_Usuario")]
        [JsonPropertyName("Id_Usuario")]
        public int Id_Usuario { get; set; }
    }
    public class Inscripcion_Asignatura {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Asignatura")]
        [JsonPropertyName("Id_Asignatura")]
        public int Id_Asignatura { get; set; }

        [JsonPropertyName("Id_Materia")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Materia")]
        [ForeignKey("Id_Asignatura")]
        public int Id_Materia { get; set; }

        [JsonPropertyName("Id_Usuario")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Usuario")]
        [ForeignKey("Id_Usuario")]
        public int Id_Usuario { get; set; }

        [JsonPropertyName("Id_Ciclo")]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Ciclo")]
        [ForeignKey("Id_Ciclo")]
        public int Id_Ciclo { get; set; }
    }
    public class Ciclos {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Ciclo")]
        [JsonPropertyName("Id_Ciclo")]
        public int Id_Ciclo { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("Descripcion")]
        [JsonPropertyName("Descripcion")]
        public string Descripcion { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("Estado")]
        [JsonPropertyName("Estado")]
        public bool Estado { get; set; }
    }
    public class Calificaciones {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Calificacion")]
        [JsonPropertyName("Id_Calificacion")]
        public int Id_Calificacion { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("Id_Asignatura")]
        [ForeignKey("Id_Asignatura")]
        [JsonPropertyName("Id_Asignatura")]
        public int Id_Asignatura { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("Nota")]        
        [JsonPropertyName("Nota")]
        public Decimal Nota { get; set; }
        //, TypeName = "System.Data.SqlTypes.SqlDouble"
        [System.ComponentModel.DataAnnotations.Schema.Column("Fecha_Calificacion")]
        [JsonPropertyName("Fecha_Calificacion")]
        public DateTime Fecha_Calificacion { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("Estado")]
        [JsonPropertyName("Estado")]
        public bool Estado { get; set; }
    }
}
