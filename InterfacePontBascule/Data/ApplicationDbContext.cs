using InterfacePontBascule.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterfacePontBascule.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Parc> Parcs { get; set; }
        public DbSet<TypeDeCamion> TypeDeCamions { get; set; }
        public DbSet<TypeDeDechet> TypeDeDechets { get; set; }
        public DbSet<TypeDeTransport> TypeDeTransports { get; set; }

        public DbSet<TypeProduit> TypeProduits { get; set; }

        public DbSet<Achat> Achats { get; set; }
    }
}