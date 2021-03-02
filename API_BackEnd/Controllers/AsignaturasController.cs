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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_BackEnd.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturasController : ControllerBase
    {
        PruebaContext Context;
        AsignaturasRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<AsignaturasController> Logger;

        private IWebHostEnvironment HostingEnv;


        public AsignaturasController(PruebaContext context, AsignaturasRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<AsignaturasController> logger,
            IWebHostEnvironment env
            ){
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<AsignaturasController>
        [HttpGet]
        public async Task<List<Asignatura>> Get(){
            return await Context.Asignaturas.OrderBy(p => p.Descripcion).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public Task<List<dynamic>> GetBy(int id){
            var nji = Context.Inscripcion_Asignaturas.Where(i => i.Id_Usuario == id).Join(Context.Asignaturas,
                a => new { Id_Asignatura = a.Id_Asignatura },
                b => new { Id_Asignatura = b.Id_Asignatura },
                (a, b) => new { Id_Asignatura = a.Id_Asignatura, Descripcion=b.Descripcion, Creditos = b.Creditos, Id_Usuario = a.Id_Usuario }
                ).Join(Context.Usuarios,
                a=> new { Id_Usuario = a.Id_Usuario },
                b=> new { Id_Usuario = b.Id_Usuario },
                (a,b) => new { Id_Asignatura = a.Id_Asignatura, Descripcion = a.Descripcion, Creditos = a.Creditos, Docente = b.Nombre_Completo })
                .OrderBy(g=>g.Descripcion);
            return nji.ToListAsync<dynamic>();
        }

        [HttpGet("{id}")]
        public async Task<Asignatura> Get(int id){
            return await Context.Asignaturas.FirstOrDefaultAsync(p => p.Id_Asignatura == id);
        }

        // POST api/<AsignaturasController>
        //[HttpPost]
        //public async Task<Asignaturas> Post([FromBody] int id){
        //    return await Context.Asignaturas.FirstOrDefaultAsync(p => p.Id_Asignatura == id);
        //}

        [HttpPost]
        public async Task<Asignatura> SaveAsignaturas([FromBody] Asignatura Asignatura){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(Asignatura))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var album = await Repository.SaveAsignatura(Asignatura);
            if (album == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return album;
        }

        // PUT api/<AsignaturasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AsignaturasController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteAsignatura(id);
        }

    }
}
