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
    public class AsignaturasRepository : EntityFrameworkRepository<PruebaContext, Asignatura>
    {
        public AsignaturasRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Asignatura entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Asignatura.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Asignatura Id</param>
        /// <returns></returns>
        public override async Task<Asignatura> Load(object AsignaturaId){
            Asignatura Asignatura = null;
            try{
                int id = (int)AsignaturaId;
                Asignatura = await Context.Asignaturas
                    .FirstOrDefaultAsync(pct => pct.Id_Asignatura == Convert.ToInt32(AsignaturaId));
                if (Asignatura != null){
                    OnAfterLoaded(Asignatura);
                }
            }
            catch (InvalidOperationException){
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Asignatura - invalid Asignatura id specified.");
                return null;
            }
            catch (Exception ex){
                // handles Sql errors
                SetError(ex);
            }
            return Asignatura;
        }

        public async Task<List<Asignatura>> GetAllAsignaturas(int page = 0, int pageSize = 15){
            IQueryable<Asignatura> Asignaturas = Context.Asignaturas
                .OrderBy(alb => alb.Descripcion);
            if (page > 0){
                Asignaturas = Asignaturas
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }

            return await Asignaturas.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedAsignatura"></param>
        /// <returns></returns>
        public async Task<Asignatura> SaveAsignatura(Asignatura postedAsignatura){
            int id = postedAsignatura.Id_Asignatura;
            Asignatura Asignatura;
            Asignatura = await Load(id);
            if (id < 1){
                Asignatura = Create();
            }
            else{
                DataUtils.CopyObjectData(postedAsignatura, Asignatura);
            }
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Asignatura;
        }

        public async Task<bool> DeleteAsignatura(int id, bool noSaveChanges = false)
        {
            //var Asignatura = await Context.Asignatura

            //if (Asignatura == null)
            //{
            //    SetError("Invalid Asignatura id.");
            //    return false;
            //}

            //Context.Asignatura.Remove(Asignatura);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Asignatura entity){
            if (entity == null){
                ValidationErrors.Add("No item was passed.");
                return false;
            }

            if (string.IsNullOrEmpty(entity.Descripcion))
                ValidationErrors.Add("Descripción de Asignatura es requerido.", "Descripcion");
            else if (entity.Creditos !=3)
                ValidationErrors.Add("Debe asignar un valor de crédito 3");
            else if (entity.Id_Usuario == 0 || entity.Id_Usuario < 1)
                ValidationErrors.Add("Especifique el docente asignado");
            return ValidationErrors.Count < 1;
        }

    }
}
