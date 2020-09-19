using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepository : IPaginacion
    {
        private readonly IFactoriConexion factoriConexion;

        public PaginacionRepository(IFactoriConexion factoriConexion)
        {
            this.factoriConexion = factoriConexion;
        }
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedure, int numeroPagina, int cantidadElemento, IDictionary<string, object> parametrosFiltros, string ordenamientoColumna)
        {
            PaginacionModel paginacion = new PaginacionModel();
            List<IDictionary<string,object>> listaReporte = null;
            int totalRegistro = 0;
            int totalPaginas = 0;
            try
            {
                var connection = this.factoriConexion.GetConnection();
                DynamicParameters parametros = new DynamicParameters();

                foreach (var param in parametrosFiltros)
                {
                    parametros.Add("@" + param.Key, param.Value);
                }

                parametros.Add("@NumeroPagina", numeroPagina);
                parametros.Add("@CantidadElementos", cantidadElemento);
                parametros.Add("@Ordenamiento", ordenamientoColumna);

                parametros.Add("@TotalRegistros", totalRegistro, DbType.Int32, ParameterDirection.Output);
                parametros.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storeProcedure, parametros, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();

                paginacion.ListadeRegistro = listaReporte;
                paginacion.NumeroPagina = parametros.Get<int>("@TotalPaginas");
                paginacion.TotalRegistro = parametros.Get<int>("@TotalRegistros");

            }
            catch (Exception e)
            {

                throw new Exception("No se pudo Ejecutar el SP",e);
            }finally
            {
                this.factoriConexion.CloseConexion();
            }
            return paginacion;
        }
    }
}
