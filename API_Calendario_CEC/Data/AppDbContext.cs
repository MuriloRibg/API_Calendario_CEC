using System;
using System.Collections.Generic;
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

        public Reserva validaReserva(int? idEvento, string nomeColuna,int idColuna ,string data, string horaInicio, string horaFim)
        {
            var reserva = Reservas
                .FromSqlRaw("SELECT * " +
                "FROM Reservas AS eventos " +
                "WHERE (id <> {0}) AND (eventos.DataInicio = {1}" +
                $" AND eventos.{nomeColuna} = {idColuna}) AND " +
                "(( {2} BETWEEN eventos.HoraInicio AND eventos.HoraFim) " +
                "OR ({3} BETWEEN eventos.HoraInicio AND eventos.HoraFim) " +
                "OR (eventos.HoraInicio BETWEEN {2} AND {3}) " +
                "OR (eventos.HoraFim BETWEEN {2} AND {3}))", idEvento, data, horaInicio, horaFim)
                .FirstOrDefaultAsync()
                .Result;
            return reserva;
        }

        public List<Reserva> innerJoinReservaAula(string tabela, int? idTurma)
        {
            var reserva = Reservas
                .FromSqlRaw("SELECT " +
                "reserva.Id, Titulo, DataInicio, HoraInicio, HoraFim, Id_Local, a.Id as Id_evento, a.Id_Turma " +
                "FROM Reservas AS reserva " +
                $"INNER JOIN {tabela} AS a ON a.Id_Reserva = reserva.Id " +
                $"WHERE a.Id_Turma = {idTurma}")
                .ToListAsync()
                .Result;
            return reserva;
        }

        public Reserva validaEvento(int? idEvento, string tabela, string nomeColuna,int idColuna ,string data, string horaInicio, string horaFim)
        {
            var reserva = Reservas
                .FromSqlRaw("SELECT " +
                "reserva.Id, Titulo, DataInicio, HoraInicio, HoraFim, Id_Local, a.Id as Id_evento " +
                "FROM Reservas AS reserva " +
                $"INNER JOIN {tabela} AS a ON a.Id_Reserva = reserva.Id " +
                "WHERE (reserva.id <> {0}) AND (reserva.DataInicio = {1}" +
                $" AND {nomeColuna} = {idColuna}) AND " +
                "(( {2} BETWEEN reserva.HoraInicio AND reserva.HoraFim) " +
                "OR ({3} BETWEEN reserva.HoraInicio AND reserva.HoraFim) " +
                "OR (reserva.HoraInicio BETWEEN {2} AND {3}) " +
                "OR (reserva.HoraFim BETWEEN {2} AND {3}))", idEvento, data, horaInicio, horaFim)
                .FirstOrDefaultAsync()
                .Result;
            return reserva;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Reserva>()
                .HasOne(reserva => reserva.Aula)
                .WithOne(aula => aula.Reserva)
                .HasForeignKey<Aula>(aula => aula.Id_Reserva);

            builder.Entity<Reserva>()
                .HasOne(reserva => reserva.Evento)
                .WithOne(evento => evento.Reserva)
                .HasForeignKey<Evento>(evento => evento.Id_Reserva);

            builder.Entity<Reserva>()
                .HasOne(reserva => reserva.Local)
                .WithMany(local => local.Reservas)
                .HasForeignKey(reserva => reserva.Id_Local);

            builder.Entity<Turma>()
               .HasOne(turma => turma.Pilar)
               .WithMany(pilar => pilar.Turmas)
               .HasForeignKey(turma => turma.Id_Pilar);

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
