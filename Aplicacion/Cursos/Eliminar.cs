using Aplicacion.ManejadorError;
using MediatR;
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
                var comentarios = this.context.Comentario.Where(x => x.CursoId == request.Id);
                foreach (var comentario in comentarios)
                {
                    this.context.Comentario.Remove(comentario);
                }

                var precio = this.context.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if (precio != null)
                {
                    this.context.Remove(precio);
                }


                var instructoresDB = this.context.CursoInstructor.Where(x => x.CursoId == request.Id).ToList();

                foreach (var instructorEliminar in instructoresDB)
                {
                    this.context.CursoInstructor.Remove(instructorEliminar);
                }

                var curso = await context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    //throw new Exception("El Curso no existe");
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new  {mensaje = "No se encontro el curso"  });
                }

                context.Remove(curso);
                var valor = await context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar el curso" });

            }
        }
    }
}
