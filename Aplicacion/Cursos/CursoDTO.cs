﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cursos
{
    public class CursoDTO
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Byte[] FotoPortada { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<InstructorDTO> Instructores { get; set; }
        public PrecioDTO Precio { get; set; }
        public ICollection<ComentarioDTO> Comentarios { get; set; }

    }
}
