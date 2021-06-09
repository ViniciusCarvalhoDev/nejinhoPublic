using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NejinhoWebApp.Models
{
    public class NejinhoDbContext : DbContext
    {
        public DbSet<Usuario> Pessoas { get; set; }
        public DbSet<Atividade> Atividades { get; set; }

        public NejinhoDbContext()
        {
        }

        public NejinhoDbContext(DbContextOptions<NejinhoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(pessoa =>
            {
                pessoa.HasIndex(u => u.Email)
                .IsUnique();
            });

            modelBuilder.Entity<Atividade>(atividade =>
            {
                atividade.HasOne(a => a.Usuario)
                .WithMany(u => u.Atividades)
                .HasForeignKey(a => a.IdUsuario)
                .HasConstraintName("AtividadesUsuariosFKConstraint");
                
            });
        }
    }
}
