using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Urun_Denetim.Models;
namespace Urun_Denetim.Data
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {
        }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Marka> Markas { get; set; }
        public DbSet<Urunler> Urunlers { get; set; }
        public DbSet<Admins> Adminses { get; set; }
        public DbSet<KresForm> KresForms { get; set; }
        public DbSet<KullaniciLoglari> KullaniciLoglaris { get; set; }

    }
}
