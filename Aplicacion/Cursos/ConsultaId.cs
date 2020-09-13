using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDTO> {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDTO>
        {
            private readonly CursosOnLineContext context;
            private readonly IMapper mapper;

            public Manejador(CursosOnLineContext _context, IMapper mapper)
            {
                context = _context;
                this.mapper = mapper;
            }
           
            public async Task<CursoDTO> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await context.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructorLink)
                    .ThenInclude(y => y.Instructor)
                    .FirstAsync(a => a.CursoId == request.Id);
                                     
                if (curso == null)
                {
                    //throw new Exception("El Curso no existe");
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }

                var cursosDto = this.mapper.Map<Curso, CursoDTO>(curso);

                return cursosDto;
             
            }
          
        }
    }
}
