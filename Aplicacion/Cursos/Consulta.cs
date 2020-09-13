using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCurso : IRequest<List<CursoDTO>>{}

        public class Manejador : IRequestHandler<ListaCurso, List<CursoDTO>>
        {
            private readonly CursosOnLineContext context;
            private readonly IMapper mapper;

            public Manejador(CursosOnLineContext _context, IMapper mapper)
            {
                context = _context;
                this.mapper = mapper;
            }

            public async Task<List<CursoDTO>> Handle(ListaCurso request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso
                    .Include(x=>x.ComentarioLista)
                    .Include(x=>x.PrecioPromocion)
                    .Include(x=>x.InstructorLink)
                    .ThenInclude(x=>x.Instructor).ToListAsync();

                var cursosDto =  this.mapper.Map<List<Curso>, List<CursoDTO>>(cursos);

                return cursosDto;
            }
        }
    }
}
