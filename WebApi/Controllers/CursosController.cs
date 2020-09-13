using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    public class CursosController : MiControllerBase
    {       
        [HttpGet]       
        public async Task<ActionResult<List<CursoDTO>>> Get()
        {
            return await mediator.Send(new Consulta.ListaCurso());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> Detalle(Guid id)
        {
            return await mediator.Send(new ConsultaId.CursoUnico{Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
           return await mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data)
        {
            data.CursoId = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
    }
}
