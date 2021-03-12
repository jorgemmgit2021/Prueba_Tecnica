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

namespace API_BackEnd.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase{
        PruebaContext Context;
        UsuariosRepository Repository;

        IServiceProvider serviceProvider;
        IConfiguration Configuration;
        private ILogger<UsuariosController> Logger;

        private IWebHostEnvironment HostingEnv;

        public UsuariosController(PruebaContext context, UsuariosRepository repository,
            IServiceProvider svcProvider,
            IConfiguration config,
            ILogger<UsuariosController> logger,
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

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<List<Usuario>> Get(){
            return await Context.Usuarios.OrderBy(d => d.Nombre_Completo).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<List<Usuario>> GetBy(int id){
            return await Context.Usuarios.Where(a=>a.Id_Usuario==id).Include(t => t.Asignaturas.Where(i => i.Id_Usuario == id)).OrderBy(d => d.Nombre_Completo).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Usuario> Get(int id){
            return await Context.Usuarios.FirstOrDefaultAsync(p => p.Id_Usuario == id);
        }

        [HttpGet("GetByIdentificacion/{identificacion}")]
        public async Task<Usuario> Find(int identificacion){
            return await Context.Usuarios.Include(i=>i.Asignaturas).FirstOrDefaultAsync(p => p.Numero_Identificacion == identificacion);
        }

        [HttpGet("GetByAsignatura/{id}")]
        public async Task<List<Usuario>> FindBy(int id){
                return await  Context.Inscripcion_Asignaturas.Where(g => g.Id_Materia == id).Join(Context.Usuarios,
                a => new {  Id_Usuario = a.Id_Usuario},
                b => new { Id_Usuario = b.Id_Usuario },
                (a, b) => new Usuario { Id_Usuario = b.Id_Usuario, Numero_Identificacion = b.Numero_Identificacion, Nombre_Completo = b.Nombre_Completo,Tipo_Usuario = b.Tipo_Usuario,Fecha = b.Fecha, Asignaturas = new List<Inscripcion_Asignatura>() }).ToListAsync<Usuario>();
        }

        // POST api/<UsuariosController>
        //[HttpPost]
        //public async Task<Usuarios> Post([FromBody] int id){
        //    return await Context.Usuarios.FirstOrDefaultAsync(p => p.Id_Paciente == id);
        //}

        [HttpPost]
        public async Task<Usuario> SaveUsuarios([FromBody] Usuario usuario){
            if (!ModelState.IsValid)
                throw new ApiException("Model binding failed.", 500);

            if (!Repository.Validate(usuario))
                throw new ApiException(Repository.ErrorMessage, 500, Repository.ValidationErrors);

            var album = await Repository.SaveUsuario(usuario);
            if (album == null)
                throw new ApiException(Repository.ErrorMessage, 500);

            return album;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await Repository.DeleteUsuario(id);
        }

    }
}
