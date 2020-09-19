using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
        Task<PaginacionModel> devolverPaginacion(string storeProcedure,
                              int numeroPagina, 
                              int cantidadElemento,
                              IDictionary<string,object> parametrosFiltros,
                              string ordenamientoColumna);
    }
}
