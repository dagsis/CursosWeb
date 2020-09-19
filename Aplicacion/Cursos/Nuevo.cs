using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {

            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public Precio Precio { get; set; }
            //public Byte[] FotoPortada { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
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
                Guid cursoId = Guid.NewGuid();

                var curso = new Curso
                {
                    CursoId = cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion ,
                    FechaCreacion = DateTime.UtcNow
                };

                context.Curso.Add(curso);

                if (request.ListaInstructor != null)
                {
                    foreach (var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = cursoId,
                            InstructorId = id
                        };
                        this.context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                var precio = new Precio
                {
                    CursoId = cursoId,
                    PrecioId = Guid.NewGuid(),
                    PrecioActual = request.Precio.PrecioActual,
                    Promocion = request.Precio.Promocion
                };

                this.context.Precio.Add(precio);

                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo Agregar el Curso" });
            }
        }
    }
}
