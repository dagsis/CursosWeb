using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator mediator;

        public CursosController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await mediator.Send(new Consulta.ListaCurso());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id)
        {
            return await mediator.Send(new ConsultaId.CursoUnico{Id =id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
           return await mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id,Editar.Ejecuta data)
        {
            data.CursoId = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
    }
}
