using Microsoft.EntityFrameworkCore;

namespace SIGD.Models
{
    public class Contexto:DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Demanda> Demandas { get; set; }
       
    }
}
