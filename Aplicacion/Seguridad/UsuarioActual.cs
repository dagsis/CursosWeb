using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
          
        }
      
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> userManager;
            private readonly IJwtGenerador jwtGenerador;
            private readonly IUsuarioSesion usuarioSesion;

            public Manejador(UserManager<Usuario> userManager,IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
            {
                this.userManager = userManager;
                this.jwtGenerador = jwtGenerador;
                this.usuarioSesion = usuarioSesion;
            }
          
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await this.userManager.FindByNameAsync(this.usuarioSesion.ObternerUsuarioSesion());
                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    UserName = usuario.UserName,
                    Token =  this.jwtGenerador.CrearToken(usuario),
                    Email = usuario.Email,
                    Imagen = null
                };
            }
        }
    }
}
