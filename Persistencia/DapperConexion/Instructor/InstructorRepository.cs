using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepository : IInstructor
    {
        private readonly IFactoriConexion factoriConexion;

        public InstructorRepository(IFactoriConexion factoriConexion)
        {
            this.factoriConexion = factoriConexion;
        }

        public Task<int> Editar(InstructorModel parametros)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Nuevo(InstructorModel parametros)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;

            var storeProcedure = "usp_obtener_Instructores";
            try
            {
                var connexion = this.factoriConexion.GetConnection();
                instructorList = await  connexion.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos", e);
            }
            finally
            {
                this.factoriConexion.CloseConexion();
            }

            return instructorList;
        }

        public Task<InstructorModel> ObtenerPorId(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
