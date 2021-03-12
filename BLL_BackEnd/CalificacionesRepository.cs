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
    public class CalificacionesRepository : EntityFrameworkRepository<PruebaContext, Calificaciones>{
        public CalificacionesRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Calificaciones entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Calificaciones.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Calificaciones Id</param>
        /// <returns></returns>
        public override async Task<Calificaciones> Load(object CicloId){
            Calificaciones Calificaciones = null;
            try{
                int id = (int)CicloId;
                Calificaciones = await Context.Calificaciones
                    .FirstOrDefaultAsync(pct => pct.Id_Calificacion == Convert.ToInt32(CicloId));
                if (Calificaciones != null){
                    OnAfterLoaded(Calificaciones);
                }
            }
            catch (InvalidOperationException)
            {
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Calificaciones - invalid Ciclo id specified.");
                return null;
            }
            catch (Exception ex)
            {
                // handles Sql errors
                SetError(ex);
            }
            return Calificaciones;
        }

        public async Task<List<Calificaciones>> GetAllCalificaciones(int page = 0, int pageSize = 15){
            IQueryable<Calificaciones> Calificaciones = Context.Calificaciones
                .OrderBy(alb => alb.Id_Calificacion);
            if (page > 0){
                Calificaciones = Calificaciones
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }

            return await Calificaciones.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedCalificaciones"></param>
        /// <returns></returns>
        public async Task<Calificaciones> SaveCalificaciones(Calificaciones postedCalificaciones){
            int id = postedCalificaciones.Id_Calificacion;
            Calificaciones Calificaciones;
            Calificaciones = await Load(id);
            if (id < 1) Calificaciones = Create();
            DataUtils.CopyObjectData(postedCalificaciones, Calificaciones);
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Calificaciones;
        }

        public async Task<bool> DeleteCalificaciones(int id, bool noSaveChanges = false){
            //var Calificaciones = await Context.Calificaciones

            //if (Calificaciones == null)
            //{
            //    SetError("Invalid Calificaciones id.");
            //    return false;
            //}

            //Context.Calificaciones.Remove(Calificaciones);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Calificaciones entity){
            if (entity == null){
                ValidationErrors.Add("No item was passed.");
                return false;
            }

            if (entity.Id_Asignatura==0)
                ValidationErrors.Add("Especificación de Asignatura es requerido.", "Descripcion");
            return ValidationErrors.Count < 1;
        }

    }
}
