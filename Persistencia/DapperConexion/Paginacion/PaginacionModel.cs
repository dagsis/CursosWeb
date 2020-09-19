using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        public List<IDictionary<string,object>> ListadeRegistro  { get; set; }
        public int TotalRegistro { get; set; }
        public int NumeroPagina { get; set; }
    }
}
