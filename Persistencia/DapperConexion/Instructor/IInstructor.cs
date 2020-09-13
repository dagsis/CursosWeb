using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(Guid Id);
        Task<int> Nuevo(InstructorModel parametros);
        Task<int> Editar(InstructorModel parametros);
        Task<int> Eliminar(Guid Id);
    }
}
