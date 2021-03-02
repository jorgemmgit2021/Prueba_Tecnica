using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_BackEnd.Models.Context{
    public class PruebaContext:DbContext{
        public PruebaContext(DbContextOptions options):
            base(options){

        }
        public PruebaContext(){

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Inscripcion_Asignatura> Inscripcion_Asignaturas { get; set; }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
        }            
    }
}