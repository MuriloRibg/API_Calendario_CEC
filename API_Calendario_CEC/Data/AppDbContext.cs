﻿using System;
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
            builder.Entity<Instrutor>()
                .HasOne(instrutor => instrutor.Pilar)
                .WithMany(pilar => pilar.Instrutor)
                .HasForeignKey(instrutor => instrutor.Id_Pilar);

            builder.Entity<Turma>()
                .HasOne(turma => turma.Pilar)
                .WithMany(pilar => pilar.Turmas)
                .HasForeignKey(turma => turma.Id_Pilar);

            builder.Entity<Disciplina>()
                .HasOne(disciplina => disciplina.Pilar)
                .WithMany(pilar => pilar.Disciplinas)
                .HasForeignKey(disciplina => disciplina.Id_Pilar);

            builder.Entity<Reserva>()
                .HasOne(reserva => reserva.Local)
                .WithMany(local => local.Reservas)
                .HasForeignKey(reserva => reserva.Id_Local);

            builder.Entity<Reserva>()
                .HasMany(reserva => reserva.Aulas)
                .WithOne(aula => aula.Reserva)
                .HasForeignKey(aula => aula.Id_Reserva);

            builder.Entity<Reserva>()
                .HasMany(reserva => reserva.Eventos)
                .WithOne(evento => evento.Reserva)
                .HasForeignKey(evento => evento.Id_Reserva);

            builder.Entity<Evento>()
                .HasOne(evento => evento.Instrutor)
                .WithMany(instrutor => instrutor.Eventos)
                .HasForeignKey(evento => evento.Id_Instrutor);

            builder.Entity<Aula>()
                .HasOne(aula => aula.Turma)
                .WithMany(turma => turma.Aulas)
                .HasForeignKey(aula => aula.Id_Turma);

            builder.Entity<Aula>()
                .HasOne(aula => aula.Instrutor)
                .WithMany(instrutor => instrutor.Aulas)
                .HasForeignKey(aula => aula.Id_Instrutor);

            builder.Entity<Aula>()
                .HasOne(aula => aula.Disciplina)
                .WithMany(disciplina => disciplina.Aulas)
                .HasForeignKey(aula => aula.Id_Disciplina);

        }

        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Pilar> Pilares { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
    }
}
