using Microsoft.EntityFrameworkCore;    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplication3_Final_OrtFlix__Modelo_final_.Models;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Context
{
    public class OrtflixDatabaseContext : DbContext
    {
        public OrtflixDatabaseContext(DbContextOptions<OrtflixDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contenido> Contenidos { get; set; }
    }
}
