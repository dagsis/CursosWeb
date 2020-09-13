using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Persistencia.DapperConexion
{
    public interface IFactoriConexion
    {
        void CloseConexion();
        IDbConnection GetConnection();
    }
}
