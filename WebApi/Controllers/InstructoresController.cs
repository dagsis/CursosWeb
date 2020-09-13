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
    }
}
