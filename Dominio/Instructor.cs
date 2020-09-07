﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Grado { get; set; }
        public byte[] FotoPerfil { get; set; }

        public ICollection<CursoInstructor> InstructorLink { get; set; }

    }
}