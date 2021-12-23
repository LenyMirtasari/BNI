using Microsoft.EntityFrameworkCore;
using SewaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<LogPenyewa> LogPenyewas { get; set; }
        public DbSet<Mobil> Mobils { get; set; }
        public DbSet<Penyewa> Penyewas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            //Penyewa - LogPenyewa
            modelBuilder.Entity<Penyewa>()
               .HasMany(a => a.LogPenyewa)
               .WithOne(b => b.Penyewa);

            //Mobil - LogPenyewa
            modelBuilder.Entity<Mobil>()
                .HasMany(c => c.LogPenyewa)
                .WithOne(e => e.Mobil);       

        }
    }
}
