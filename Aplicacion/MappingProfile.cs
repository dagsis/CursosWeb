using Aplicacion.Cursos;
using AutoMapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDTO>().ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructorLink.Select(a => a.Instructor).ToList()))
            .ForMember(x => x.Comentarios, y => y.MapFrom(z => z.ComentarioLista))
            .ForMember(x => x.Precio, y => y.MapFrom(y => y.PrecioPromocion));
            CreateMap<CursoInstructor, CursoIntructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<Precio, PrecioDTO>();
        }
    }
}
