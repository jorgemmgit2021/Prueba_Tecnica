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
    public class CiclosRepository : EntityFrameworkRepository<PruebaContext, Ciclos>{
        public CiclosRepository(PruebaContext context)
            : base(context)
        { }

        protected override void OnAfterCreated(Ciclos entity)
        {
            base.OnAfterCreated(entity);
        }

        /// <summary>
        /// Loads and individual Ciclos.
        /// 
        /// Implementation is custom not using base.Load()
        /// in order to include related entities
        /// </summary>
        /// <param name="objId">Ciclos Id</param>
        /// <returns></returns>
        public override async Task<Ciclos> Load(object CicloId){
            Ciclos Ciclos = null;
            try{
                int id = (int)CicloId;
                Ciclos = await Context.Ciclos
                    .FirstOrDefaultAsync(pct => pct.Id_Ciclo == Convert.ToInt32(CicloId));
                if (Ciclos != null){
                    OnAfterLoaded(Ciclos);
                }
            }
            catch (InvalidOperationException){
                // Handles errors where an invalid Id was passed, but SQL is valid
                SetError("Couldn't load Ciclos - invalid Ciclo id specified.");
                return null;
            }
            catch (Exception ex){
                // handles Sql errors
                SetError(ex);
            }
            return Ciclos;
        }

        public async Task<List<Ciclos>> GetAllCiclos(int page = 0, int pageSize = 15){
            IQueryable<Ciclos> Ciclos = Context.Ciclos
                .OrderBy(alb => alb.Descripcion);
            if (page > 0){
                Ciclos = Ciclos
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }

            return await Ciclos.ToListAsync();
        }

        /// <summary>
        /// This code is rather complex as EF7 can't work out
        /// the related entity updates for artist and tracks, 
        /// so this code manually  updates artists and tracks 
        /// from the saved entity using code.
        /// </summary>
        /// <param name="postedCiclos"></param>
        /// <returns></returns>
        public async Task<Ciclos> SaveCiclos(Ciclos postedCiclos){
            int id = postedCiclos.Id_Ciclo;
            Ciclos Ciclos;
            Ciclos = await Load(id);
            if (id < 1){
                Ciclos = Create();
                DataUtils.CopyObjectData(postedCiclos,Ciclos);
            }
            else{
                DataUtils.CopyObjectData(postedCiclos, Ciclos);
            }
            //now lets save it all
            if (!await SaveAsync()) return null;
            else
                return Ciclos;
        }

        public async Task<bool> DeleteCiclos(int id, bool noSaveChanges = false){
            //var Ciclos = await Context.Ciclos

            //if (Ciclos == null)
            //{
            //    SetError("Invalid Ciclos id.");
            //    return false;
            //}

            //Context.Ciclos.Remove(Ciclos);


            //if (!noSaveChanges)
            //{
            //    var result = await SaveAsync();

            //    return result;
            //}

            //return true;
            return false;
        }

        protected override bool OnValidate(Ciclos entity){
            if (entity == null){
                ValidationErrors.Add("No item was passed.");
                return false;
            }

            if (string.IsNullOrEmpty(entity.Descripcion))
                ValidationErrors.Add("Descripción de Ciclos es requerido.", "Descripcion");
            return ValidationErrors.Count < 1;
        }

    }
}
