using MediatR;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class Consulta
    {
        public class ListaInstructores : IRequest<List<InstructorModel>> { }

        public class Manejador : IRequestHandler<ListaInstructores, List<InstructorModel>>
        {
            private readonly IInstructor instructorRepository;

            public Manejador(IInstructor instructorRepository)
            {
                this.instructorRepository = instructorRepository;
            }

            public async Task<List<InstructorModel>> Handle(ListaInstructores request, CancellationToken cancellationToken)
            {
                var resultado =  await this.instructorRepository.ObtenerLista();
                return resultado.ToList();
            }
        }
    }
}
