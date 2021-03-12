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
    public class CiclosController : ControllerBase
    {
        PruebaContext Context;
        CiclosRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<CiclosController> Logger;

        private IWebHostEnvironment HostingEnv;


        public CiclosController(PruebaContext context, CiclosRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<CiclosController> logger,
            IWebHostEnvironment env
            )
        {
            Context = context;
            Repository = repository;
            serviceProvider = svcProvider;
            Configuration = config;
            Logger = logger;
            HostingEnv = env;
        }

        // GET: api/<CiclosController>
        [HttpGet]
        public async Task<List<Ciclos>> Get(){
            return await Context.Ciclos.OrderBy(p => p.Descripcion).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public Task<List<Ciclos>> GetBy(int id){
            //var nji = Context.Inscripcion_Ciclos.Where(i => i.Id_Usuario == id).Join(Context.Ciclos,
            //    a => new { Id_Asignatura = a.Id_Asignatura },
            //    b => new { Id_Asignatura = b.Id_Asignatura },
            //    (a, b) => new { Id_Asignatura = a.Id_Asignatura, Descripcion = b.Descripcion, Creditos = b.Creditos, Id_Usuario = a.Id_Usuario }
            //    ).Join(Context.Usuarios,
            //    a => new { Id_Usuario = a.Id_Usuario },
            //    b => new { Id_Usuario = b.Id_Usuario },
            //    (a, b) => new { Id_Asignatura = a.Id_Asignatura, Descripcion = a.Descripcion, Creditos = a.Creditos, Docente = b.Nombre_Completo })
            //    .OrderBy(g => g.Descripcion);
            return Context.Ciclos.Where(c=>c.Id_Ciclo==id).ToListAsync<Ciclos>();
        }

        [HttpGet("{id}")]
        public async Task<Ciclos> Get(int id){
            return await Context.Ciclos.FirstOrDefaultAsync(p => p.Id_Ciclo == id);
        }

        // POST api/<CiclosController>
        //[HttpPost]
        //public async Task<Ciclos> Post([FromBody] int id){
        //    return await Context.Ciclos.FirstOrDefaultAsync(p => p.Id_Asignatura == id);
        //}

        [HttpPost]
        public async Task<Ciclos> SaveCiclos([FromBody] Ciclos Ciclos ){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(Ciclos))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var registro = await Repository.SaveCiclos(Ciclos);
            if (registro == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return registro;
        }

        // PUT api/<CiclosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value){
        }

        // DELETE api/<CiclosController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id){
            return await Repository.DeleteCiclos(id);
        }
    }
}
