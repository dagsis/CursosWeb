using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly CursosOnLineContext context;

        public HomeController(CursosOnLineContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IEnumerable<Curso> Get()
        {            
            return context.Curso.ToList();
        }
    }
}
