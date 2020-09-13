using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
       [HttpPost("login")]
       public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            return await mediator.Send(parametros);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros)
        {
            return await mediator.Send(parametros);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioData>> UsuarioActual()
        {
            return await mediator.Send(new UsuarioActual.Ejecuta());
        }

    }
}
