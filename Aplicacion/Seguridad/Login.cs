using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
      
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly SignInManager<Usuario> signInManager;
            private readonly IJwtGenerador jwtGenerador;

            public Manejador(UserManager<Usuario> userManager,SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.jwtGenerador = jwtGenerador;
            }


            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await this.userManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.Unauthorized);
                }

                var resultado = await this.signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                if (resultado.Succeeded)
                {
                    return new UsuarioData { 
                         NombreCompleto = usuario.NombreCompleto,
                         Token = this.jwtGenerador.CrearToken(usuario),
                         UserName = usuario.UserName,
                         Email = usuario.Email,
                         Imagen = null
                    };
                }
                throw new ManejadorExepcion(HttpStatusCode.Unauthorized);
            }

        }
    }
}
