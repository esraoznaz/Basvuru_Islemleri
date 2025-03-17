using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Basvurular.Entities;


namespace Basvurular.DataAccess
{
    public class ApplicationDbContext: DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Marka> Markas { get; set; }
        public DbSet<Urunler> Urunlers { get; set; }
        public DbSet<Admins> Adminses { get; set; }
        public DbSet<KresForm> KresForms { get; set; }
        public DbSet<KullaniciLoglari> KullaniciLoglaris { get; set; }
		public DbSet<Ilce> Ilces { get; set; }
		public DbSet<Mahalle> Mahalles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-3H9G77VD\\SQLEXPRESS;Initial Catalog=Basvurular;Integrated Security=True;TrustServerCertificate=True",
                    b => b.MigrationsAssembly("Urun_Denetim"));
            }
        }

    }
}
