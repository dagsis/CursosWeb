using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Comentarios
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            public string Alumno { get; set; }
            public int Puntaje { get; set; }
            public string ComentarioTexto { get; set; }
            public Guid CursoId { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Alumno).NotEmpty();
                RuleFor(x => x.Puntaje).NotEmpty();
                RuleFor(x => x.ComentarioTexto).NotEmpty();
                RuleFor(x => x.CursoId).NotEmpty();
            }
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

                var comentario = new Comentario
                {
                    ComentarioId = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    ComentarioTexto = request.ComentarioTexto,
                    Puntaje = request.Puntaje,
                    CursoId = request.CursoId,
                    FechaCreacion = DateTime.UtcNow
                };

                context.Comentario.Add(comentario);

                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo Agregar el Comentario" });
            }
        }
    }
}
