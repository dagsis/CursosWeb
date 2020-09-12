﻿using System;
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
    }
}
