using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta,UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly CursosOnLineContext _context;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IPasswordHasher<Usuario> _passwordHasher;

            public Manejador(UserManager<Usuario> userManager, CursosOnLineContext context, IJwtGenerador jwtGenerador, IPasswordHasher<Usuario> passwordHasher)
            {
                this._userManager = userManager;
                this._context = context;
                this._jwtGenerador = jwtGenerador;
                this._passwordHasher = passwordHasher;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(request.Username);
                if (usuario == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensasaje = "El Usuario no existe" });
                }

                var resultado = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();
                if (resultado)
                {
                    throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensasaje = "El Email Existe en la base de datos" });
                }
                usuario.NombreCompleto = request.Nombre + ' ' + request.Apellidos;
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario,request.Password);
                usuario.Email = request.Email;
                var resultadoUpdate = await _userManager.UpdateAsync(usuario);

                var resultadoRoles = (List<string>) await _userManager.GetRolesAsync(usuario);

                if (resultadoUpdate.Succeeded){
                    return new UsuarioData {
                        NombreCompleto = usuario.NombreCompleto,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        Token = _jwtGenerador.CrearToken(usuario, resultadoRoles)
                    };
                }

                throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensasaje = "No se pudo actualizar el usuario" });
            }
        }
    }
}
