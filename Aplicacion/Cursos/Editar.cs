using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
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

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            public List<Guid> ListaInstructor { get; set; }

            public decimal? Precio { get; set; }
            public decimal? PrecioPromocion { get; set; }

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

                var curso = await context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }

            
                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                var precio = this.context.Precio.Where(x => x.CursoId == curso.CursoId).FirstOrDefault();
                if (precio != null)
                {
                    precio.PrecioActual = request.Precio ?? precio.PrecioActual;
                    precio.Promocion = request.PrecioPromocion ?? precio.Promocion;
                }

                if (request.ListaInstructor != null)
                {
                    if (request.ListaInstructor.Count > 0)
                    {
                        // Eliminar de la base de datos
                        var instructoresDB = this.context.CursoInstructor.Where(x => x.CursoId == request.CursoId).ToList();
                        foreach (var instructorEliminar in instructoresDB)
                        {
                            this.context.CursoInstructor.Remove(instructorEliminar);
                        }

                        // Agregar los que vienen del cliente
                        foreach (var id in request.ListaInstructor)
                        {
                            var cursoInstructor = new CursoInstructor
                            {
                                CursoId = request.CursoId,
                                InstructorId = id
                            };
                            this.context.CursoInstructor.Add(cursoInstructor);
                        }
                    }
                  
                }

                //context.Curso.Update(curso);
                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo Editar el Curso" });
            }
        }
    }
}
