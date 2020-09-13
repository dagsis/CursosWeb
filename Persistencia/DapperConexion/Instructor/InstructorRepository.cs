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

        public async Task<int> Editar(Guid instructorId, string nombre, string apellido, string grado)
        {
            var resultado = 0;

            try
            {
                var connexion = this.factoriConexion.GetConnection();
                resultado = await connexion.ExecuteAsync("usp_Instructor_editar", new
                {
                    InstructorId = instructorId,
                    Nombre = nombre,
                    Apellido = apellido,
                    Grado = grado
                }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la edicion de datos", e);
            }
            finally
            {
                this.factoriConexion.CloseConexion();
            }

            return resultado;
        }

        public Task<int> Eliminar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Nuevo(string nombre,string apellido, string grado)
        {
            var resultado  = 0;

            try
            {
                var connexion = this.factoriConexion.GetConnection();
                resultado = await   connexion.ExecuteAsync("usp_Instructor_nuevo", new
                {
                    InstructorId = Guid.NewGuid(),
                    Nombre = nombre,
                    Apellido =  apellido,
                    Grado = grado
                },commandType : CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en el ingreso de datos", e);
            }
            finally
            {
                this.factoriConexion.CloseConexion();
            }

            return resultado;

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
