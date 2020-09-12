using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnLineContext context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Carlos D Agostino", UserName = "dagsis", Email = "dagsis@dagsis.com.ar" };
                await userManager.CreateAsync(usuario, "Zulu1234.");

            }
        }
    }
}
