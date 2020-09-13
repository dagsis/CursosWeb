using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.DapperConexion
{
    public class FactoryConexion : IFactoriConexion
    {
        private IDbConnection connection;
        private readonly IOptions<ConexionConfiguracion> configs;

        public FactoryConexion(IOptions<ConexionConfiguracion> configs)
        {
            this.configs = configs;
        }

        public void CloseConexion()
        {
            if (this.connection == null && this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }       
        }

        public IDbConnection GetConnection()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.configs.Value.DefaultConnection);
            }
            if (connection.State != ConnectionState.Open)
            {
                this.connection.Open();
            }
            return this.connection;
        }
    }
}
