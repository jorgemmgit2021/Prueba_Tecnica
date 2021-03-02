using BLL_BackEnd.Models;
using BLL_BackEnd.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westwind.BusinessObjects;
using Westwind.Utilities;

namespace BLL_BackEnd
{
    public class UsuariosRepository : EntityFrameworkRepository<PruebaContext, Usuario>{
        public UsuariosRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Usuario entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Usuario.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Usuario Id</param>
        /// <returns></returns>
        public override async Task<Usuario> Load(object UsuarioId){
            Usuario Usuario = null;
            try
            {
                int id = (int)UsuarioId;
                Usuario = await Context.Usuarios
                    .FirstOrDefaultAsync(pct => pct.Id_Usuario == Convert.ToInt32(UsuarioId));
                if (Usuario != null){
                    OnAfterLoaded(Usuario);
                }
            }
            catch (InvalidOperationException){
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Usuario - invalid Usuario id specified.");
                return null;
            }
            catch (Exception ex){
                // handles Sql errors
                SetError(ex);
            }
            return Usuario;
        }

        public async Task<List<Usuario>> GetAllUsuarios(int page = 0, int pageSize = 15){
            IQueryable<Usuario> Usuarios = Context.Usuarios
                .OrderBy(alb => alb.Nombre_Completo);
            if (page > 0){
                Usuarios = Usuarios
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }

            return await Usuarios.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedUsuario"></param>
        /// <returns></returns>
        public async Task<Usuario> SaveUsuario(Usuario postedUsuario){
            int id = postedUsuario.Id_Usuario;
            Usuario Usuario;
            Usuario = await Load(id);
            if (id < 1){
                Usuario = Create();
                Usuario.Asignaturas = new List<Inscripcion_Asignatura>();
                DataUtils.CopyObjectData(postedUsuario, Usuario, "Asignaturas");
                postedUsuario.Asignaturas.ForEach((p) => { var c = Create<Inscripcion_Asignatura>(); if (p.Id_Asignatura == 0) { c.Id_Materia = p.Id_Materia; c.Id_Asignatura = p.Id_Asignatura; Usuario.Asignaturas.Add(c); } });
            }
            else
            {
                DataUtils.CopyObjectData(postedUsuario, Usuario, "Asignaturas");
                postedUsuario.Asignaturas.ForEach((p) => {
                    Inscripcion_Asignatura c;
                    if (p.Id_Asignatura == 0){
                        c = Create<Inscripcion_Asignatura>();
                        c.Id_Usuario = p.Id_Usuario; c.Id_Materia = p.Id_Materia; Usuario.Asignaturas.Add(c);
                    }
                    else{
                        c = Usuario.Asignaturas.FirstOrDefault(i => i.Id_Asignatura == p.Id_Asignatura) ?? new Inscripcion_Asignatura();
                        DataUtils.CopyObjectData(p, c);
                    }
                });
            }
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Usuario;
        }

        public async Task<bool> DeleteUsuario(int id, bool noSaveChanges = false){
            //var Usuario = await Context.Usuario

            //if (Usuario == null)
            //{
            //    SetError("Invalid Usuario id.");
            //    return false;
            //}

            //Context.Usuario.Remove(Usuario);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Usuario entity){
            if (entity == null)
            {
                ValidationErrors.Add("No item was passed.");
                return false;
            }
            Func<List<Inscripcion_Asignatura>,bool> a  =(List<Inscripcion_Asignatura> _L) => {
                var n = _L.Join(Context.Asignaturas,
                    i => new { Id_Asignatura = i.Id_Materia },
                    g => new { Id_Asignatura = g.Id_Asignatura },
                    (i, g) => new { Id_Asignatura = i.Id_Asignatura, Id_Docente = g.Id_Usuario }).Select(b => b.Id_Docente).Distinct();
                return n.Count() != _L.Count;
            };
            if (string.IsNullOrEmpty(entity.Nombre_Completo))
                ValidationErrors.Add("Nombre de usuario es requerido.", "Nombre_Completo");
            else if (entity.Tipo_Usuario==1 && entity.Asignaturas.Count != 3)
                ValidationErrors.Add("Debe registrar asignaturas ");
            else if (entity.Numero_Identificacion < 99999 && entity.Numero_Identificacion > 99999999)
                ValidationErrors.Add("Longitud del número de identificación incorrecta");
            else if (a(entity.Asignaturas))
                ValidationErrors.Add("No puede registrar clases con el mismo docente");
            return ValidationErrors.Count < 1;
        }

    }
}
