using Aplicacion.ManejadorError;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class ConsultaId
    {
        public class Instructor : IRequest<InstructorModel>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Instructor, InstructorModel>
        {
            private readonly IInstructor instructorRepository;

            public Manejador(IInstructor instructorRepository)
            {
                this.instructorRepository = instructorRepository;
            }

            public async Task<InstructorModel> Handle(Instructor request, CancellationToken cancellationToken)
            {
                var resultado = await this.instructorRepository.ObtenerPorId(request.Id);
                if (resultado == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No Existe el instructor" });
                }
                return resultado;
            }
        }
    }
}
