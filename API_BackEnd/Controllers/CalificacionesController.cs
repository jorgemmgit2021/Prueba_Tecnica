using API_BackEnd.API;
using BLL_BackEnd;
using BLL_BackEnd.Models;
using BLL_BackEnd.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        PruebaContext Context;
        CalificacionesRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<CalificacionesController> Logger;

        private IWebHostEnvironment HostingEnv;


        public CalificacionesController(PruebaContext context, CalificacionesRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<CalificacionesController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<CalificacionesController>
        [HttpGet]
        public async Task<List<Calificaciones>> Get(){
            return await Context.Calificaciones.OrderBy(p => p.Id_Calificacion).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public Task<List<Calificaciones>> GetBy(int id){
            //var nji = Context.Inscripcion_Calificaciones.Where(i => i.Id_Usuario == id).Join(Context.Calificaciones,
            //    a => new { Id_Asignatura = a.Id_Asignatura },
            //    b => new { Id_Asignatura = b.Id_Asignatura },
            //    (a, b) => new { Id_Asignatura = a.Id_Asignatura, Descripcion = b.Descripcion, Creditos = b.Creditos, Id_Usuario = a.Id_Usuario }
            //    ).Join(Context.Usuarios,
            //    a => new { Id_Usuario = a.Id_Usuario },
            //    b => new { Id_Usuario = b.Id_Usuario },
            //    (a, b) => new { Id_Asignatura = a.Id_Asignatura, Descripcion = a.Descripcion, Creditos = a.Creditos, Docente = b.Nombre_Completo })
            //    .OrderBy(g => g.Descripcion);
            return Context.Calificaciones.Where(c => c.Id_Calificacion == id).ToListAsync<Calificaciones>();
        }

        [HttpGet("{id}")]
        public async Task<Calificaciones> Get(int id){
            return await Context.Calificaciones.FirstOrDefaultAsync(p => p.Id_Calificacion == id);
        }

        // POST api/<CalificacionesController>
        //[HttpPost]
        //public async Task<Calificaciones> Post([FromBody] int id){
        //    return await Context.Calificaciones.FirstOrDefaultAsync(p => p.Id_Asignatura == id);
        //}

        [HttpPost]
        public async Task<Calificaciones> SaveCalificaciones([FromBody] Calificaciones Calificaciones){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);
            if (!Repository.Validate(Calificaciones))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);
            var registro = await Repository.SaveCalificaciones(Calificaciones);
            if (registro == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return registro;
        }

        // PUT api/<CalificacionesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CalificacionesController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteCalificaciones(id);
        }

    }
}
