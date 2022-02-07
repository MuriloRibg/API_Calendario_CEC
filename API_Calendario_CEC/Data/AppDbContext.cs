using System;
using API_Calendario_CEC.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Calendario_CEC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            :base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Turma>()
                .HasOne(turma => turma.Pilar)
                .WithMany(pilar => pilar.Turmas)
                .HasForeignKey(turma => turma.Id_Pilar);
        }

        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Pilar> Pilares { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Turma> Turmas { get; set; }
    }
}
