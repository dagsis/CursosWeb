using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cursos
{
    public class InstructorDTO
    {
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Grado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
