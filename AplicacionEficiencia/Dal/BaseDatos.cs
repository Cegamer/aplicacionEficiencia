using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AplicacionEficiencia.Modelos;

namespace AplicacionEficiencia.Dal
{
    internal sealed class ConexionContext : DbContext
    {
        //AQUI
        public string database = "db.db";
        public DbSet<Programa>? Programas { get; private set; }

        //public DbSet<Perfil> Perfiles { get; private set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: $"Filename={database}",
                sqliteOptionsAction: op =>
                {
                    op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                }
            );
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Programa>().ToTable("Programa");
            modelBuilder.Entity<Programa>(entity =>
            {
                entity.HasKey(e => e.id);
            });

            /*
            modelBuilder.Entity<Perfil>().ToTable("Perfil");
            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.id);

            });
            */
            base.OnModelCreating(modelBuilder);
        }
    }
}
