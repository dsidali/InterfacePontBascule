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
        public DbSet<ReceptionRondBeton> ReceptionRondBetons { get; set; }
        public DbSet<SortieRondBeton> SortieRondBetons { get; set; }
        public DbSet<Pesage> Pesages { get; set; }

        public DbSet<SortieTransfertDechet> SortieTransfertDechets { get; set; }

        public DbSet<SortieTransfertRondBeton> SortieTransfertRondBetons { get; set; }

        public DbSet<ReceptionTransfertDechet> ReceptionTransfertDechets { get; set; }

        public DbSet<ReceptionTransfertRondBeton> ReceptionTransfertRondBetons { get; set; }

        public DbSet<ComPort> ComPorts { get; set; }
    }
}