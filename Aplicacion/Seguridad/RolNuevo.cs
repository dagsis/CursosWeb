using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolNuevo
    {

        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public Manejador(RoleManager<IdentityRole> roleManager)
            {
                this._roleManager = roleManager;
            }


            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.RoleExistsAsync(request.Nombre);
                if (role == false)
                {
                    var resultado = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));
                    if (resultado.Succeeded)
                    {
                        return Unit.Value;
                    }
                }

                throw new Exception("No se pudo insertar el Rol o Existe");
            }

        }
    }
}