using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Comentarios
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnLineContext context;

            public Manejador(CursosOnLineContext _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var comentario = this.context.Comentario.FindAsync(request.Id);
                if (comentario == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "Comentario no encontrado" });
                }

                this.context.Remove(comentario);

                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar el comentario" });

            }
        }
    }
}
