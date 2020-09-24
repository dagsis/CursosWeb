using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
   
    public class UsuarioController : MiControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            return await mediator.Send(parametros);
        }

        [HttpPost("registrar")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros)
        {
            return await mediator.Send(parametros);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioData>> UsuarioActual()
        {
            return await mediator.Send(new UsuarioActual.Ejecuta());
        }

        [HttpPost("crearrol")]
        [Authorize]
        public async Task<ActionResult<Unit>> CrearRol(RolNuevo.Ejecuta parametro)
        {
            return await mediator.Send(parametro);
        }

        [HttpDelete("eliminarrol")]
        [Authorize]
        public async Task<ActionResult<Unit>> EliminarRol(RolEliminar.Ejecuta parametro)
        {
            return await mediator.Send(parametro);
        }

        [HttpGet("traerroles")]
        public async Task<ActionResult<List<IdentityRole>>> TraerRoles()
        {
            return await mediator.Send(new RolListar.Ejecuta());
        }

        [HttpPost("agregarroleusuario")]
        [Authorize]
        public async Task<ActionResult<Unit>> AgregarRoleUsuario(UsuarioRolAgregar.Ejecuta parametro)
        {
            return await mediator.Send(parametro);
        }

        [HttpPost("eliminarrolusuario")]
        [Authorize]
        public async Task<ActionResult<Unit>> EliminarRolUsuario(UsuarioRolEliminar.Ejecuta parametro)
        {
            return await mediator.Send(parametro);
        }

        [HttpGet("traerusuarioroles/{username}")]
        public async Task<ActionResult<List<string>>> TraerUsuarioRoles(string username)
        {
            return await mediator.Send(new UsuarioRolTraer.Ejecuta {Username = username });
        }

    }
}
