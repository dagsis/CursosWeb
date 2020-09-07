using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCurso : IRequest<List<Curso>>{}

        public class Manejador : IRequestHandler<ListaCurso, List<Curso>>
        {
            private readonly CursosOnLineContext context;

            public Manejador(CursosOnLineContext _context)
            {
                context = _context;
            }

            public async Task<List<Curso>> Handle(ListaCurso request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso.ToListAsync();
                return cursos;
            }
        }
    }
}
