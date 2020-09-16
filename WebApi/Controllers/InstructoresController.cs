using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class InstructoresController : MiControllerBase
    {
          [HttpGet]
          public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores()
          {
                return await mediator.Send(new Consulta.ListaInstructores());
          }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorModel>> ObtenerInstructor(Guid id)
        {
            return await mediator.Send(new ConsultaId.Instructor { Id = id});
        }

        [HttpPost]
         public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
         {
            return await mediator.Send(data);
         }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data)
        {
            data.InstructorId = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
    }
}
