using Aplicacion.Instructores;
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
    }
}
