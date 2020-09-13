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
        Task<int> Nuevo(string nombre, string apellido, string grado);
        Task<int> Editar(Guid instructorId, string nombre, string apellido, string grado);
        Task<int> Eliminar(Guid Id);
    }
}
