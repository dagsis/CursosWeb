using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class RolListar
    {
        public class Ejecuta : IRequest<List<IdentityRole>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            private readonly CursosOnLineContext _context;

            public Manejador(CursosOnLineContext context)
            {
                this._context = context;
            }

            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultado = await _context.Roles.ToListAsync();
                return resultado.ToList();
            }
        }
    }
}
